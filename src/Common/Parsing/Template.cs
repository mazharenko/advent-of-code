using System.Runtime.CompilerServices;
using Superpower.Model;

// ReSharper disable once CheckNamespace
namespace Superpower.Parsers;

public static class Template
{
	public static TextParser<T> Matching<T>(TemplateParserStringHandler<T> stringHandler)
	{
		if (typeof(T).IsAssignableTo(typeof(ITuple)))
			return stringHandler.GetParser().Select(t => (T)t);
		return stringHandler.GetParser().Select(t => (T)t[0]!);
}
	public static TextParser<(T1, T2)> Matching<T1, T2>(TemplateParserStringHandler<(T1, T2)> stringHandler)
	{
		return stringHandler.GetParser().Select(t => ((T1, T2))t);
	}
	public static TextParser<(T1, T2, T3)> Matching<T1, T2, T3>(TemplateParserStringHandler<(T1, T2, T3)> stringHandler)
	{
		return stringHandler.GetParser().Select(t => ((T1, T2, T3))t);
	}
	public static TextParser<(T1, T2, T3, T4)> Matching<T1, T2, T3, T4>(TemplateParserStringHandler<(T1, T2, T3, T4)> stringHandler)
	{
		return stringHandler.GetParser().Select(t => ((T1, T2, T3, T4))t);
	}
	public static TextParser<(T1, T2, T3, T4, T5)> Matching<T1, T2, T3, T4, T5>(TemplateParserStringHandler<(T1, T2, T3, T4, T5)> stringHandler)
	{
		return stringHandler.GetParser().Select(t => ((T1, T2, T3, T4, T5))t);
	}
}

[InterpolatedStringHandler]
public class TemplateParserStringHandler<T>
{
	private TextParser<ITuple> accParser = span => Result.Value((ITuple)ValueTuple.Create(), span, span);

	public TemplateParserStringHandler(int literalLength, int formattedCount)
	{
		if (typeof(T).IsAssignableTo(typeof(ITuple)))
		{
			var argsLength = typeof(T).GenericTypeArguments.Length;
			if (argsLength != formattedCount)
				throw new ArgumentException(
					$"The template contains {formattedCount} formatted values, but type {typeof(T)} has {argsLength} arguments.");
		}
		else if (1 != formattedCount)
			throw new ArgumentException(
				$"The template contains {formattedCount} formatted values, but {typeof(T)} is not a tuple type.");
	}

	public void AppendLiteral(string value)
	{
		accParser = accParser.ThenIgnore(Span.EqualTo(value));
	}

	public void AppendFormatted<T1>(TextParser<T1> t) where T1 : notnull
	{
		accParser = accParser.Then(x => t.Select(ITuple (y) =>
		{
			var tupleArgs = Enumerable.Range(0, x.Length)
				.Select(i => x[i]!)
				.Append(y).ToArray();

			var createMethod = typeof(ValueTuple).GetMethods()
				.Single(m1 => m1.Name == nameof(ValueTuple.Create) && m1.GetParameters().Length == tupleArgs.Length)
				.MakeGenericMethod(tupleArgs.Select(gg => gg.GetType()).ToArray());
			return (ITuple)createMethod.Invoke(null, tupleArgs.ToArray())!;
		}));
	}

	public TextParser<ITuple> GetParser()
	{
		return accParser;
	}
}