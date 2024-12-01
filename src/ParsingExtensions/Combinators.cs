using System.Text.RegularExpressions;
using Superpower.Model;
// ReSharper disable once CheckNamespace
using Superpower.Parsers;

// ReSharper disable once CheckNamespace
namespace Superpower;

public static partial class Combinators
{
	/// <summary>
	/// Construct a parser that applies <paramref name="first" />, applies <paramref name="second" /> and returns their results as a tuple.
	/// </summary>
	/// <typeparam name="T">The type of value being parsed by <paramref name="first" />.</typeparam>
	/// <typeparam name="U">The type of value being parsed by <paramref name="first" />.</typeparam>
	/// <param name="first">The first parser.</param>
	/// <param name="second">The second parser.</param>
	/// <returns>The resulting parser.</returns>
	public static TextParser<(T, U)> Then<T, U>(this TextParser<T> first, TextParser<U> second)
	{
		return first.Then(t => second.Select(u => (t, u)));
	}

	/// <summary>
	/// Construct a parser that matches <paramref name="parser"/> zero or more times, delimited by spaces.
	/// </summary>
	/// <typeparam name="T">The type of value being parsed.</typeparam>
	/// <param name="parser">The parser.</param>
	/// <returns>The resulting parser.</returns>
	public static TextParser<T[]> ManyDelimitedBySpaces<T>(this TextParser<T> parser)
	{
		return parser.ManyDelimitedBy(SpanX.Space);
	}

	/// <summary>
	/// Construct a parser that matches one or more instances of applying <paramref name="parser"/>, delimited by space.
	/// </summary>
	/// <typeparam name="T">The type of value being parsed.</typeparam>
	/// <typeparam name="U">The type of the resulting value.</typeparam>
	/// <param name="parser">The parser.</param>
	/// <returns>The resulting parser.</returns>
	public static TextParser<T[]> AtLeastOnceDelimitedBySpaces<T>(this TextParser<T> parser)
	{
		return parser.AtLeastOnceDelimitedBy(SpanX.Space);
	}
	
	[GeneratedRegex("^.*?(?=(\r\n|\n){2})", RegexOptions.Singleline)]
	private static partial Regex TextUntilBlockSeparatorRegex();
	public static TextParser<T> Block<T>(this TextParser<T> parser)
	{
		TextParser<TextSpan> untilSeparatorParser =  input =>
		{
			if (input.Length == 0)
				return Result.Empty<TextSpan>(input, ["some text until block separator"]);
			var m = TextUntilBlockSeparatorRegex().Match(input.Source!, input.Position.Absolute, input.Length);
			if (!m.Success)
				return Result.Value(input, input, TextSpan.Empty);

			var remainder = input.Skip(m.Length);
			return Result.Value(input.First(m.Length), input, remainder);
		};

		return untilSeparatorParser.Apply(parser).ThenIgnore(SpanX.BlockSeparator.Optional());
	}

	public static TextParser<(T, U)> ThenBlock<T, U>(this TextParser<T> first, TextParser<U> second)
	{
		return first.Then(second.Block());
	}

	public static TextParser<U> ThenBlock<T, U>(this TextParser<T> first, Func<T, TextParser<U>> second)
	{
		return first.Then(t => second(t).Block());
	}
	
	public static TextParser<T[]> Lines<T>(this TextParser<T> parser)
	{
		return parser.ManyDelimitedBy(SpanX.NewLine);
	}
	
	public static TextParser<T> ThenIgnore<T, U>(this TextParser<T> first, TextParser<U> second)
	{
		return first.Then(x => second.Select(_ => x));
	}

	public static TextParser<T[]> Blocks<T>(this TextParser<T> parser)
	{
		return parser.Block().Many();
	}

	/// <summary>
	/// Construct a parser that takes the result of <paramref name="parser" /> and converts its value using <paramref name="selector" />.
	/// </summary>
	/// <param name="parser">The parser.</param>
	/// <param name="selector">A mapping from the tuple result.</param>
	/// <returns>The resulting parser.</returns>
	public static TextParser<R> Select<T, U, R>(this TextParser<(T, U)> parser, Func<T, U, R> selector)
	{
		return parser.Select(x => selector(x.Item1, x.Item2));
	}

	/// <summary>
	/// Construct a parser that takes the result of <paramref name="parser" /> and converts its value using <paramref name="selector" />.
	/// </summary>
	/// <param name="parser">The parser.</param>
	/// <param name="selector">A mapping from the tuple result.</param>
	/// <returns>The resulting parser.</returns>
	public static TextParser<R> Select<T1, T2, T3, R>(this TextParser<(T1, T2, T3)> parser, Func<T1, T2, T3, R> selector)
	{
		return parser.Select(x => selector(x.Item1, x.Item2, x.Item3));
	}

	/// <summary>
	/// Construct a parser that takes the result of <paramref name="parser" /> and converts its value using <paramref name="selector" />.
	/// </summary>
	/// <param name="parser">The parser.</param>
	/// <param name="selector">A mapping from the tuple result.</param>
	/// <returns>The resulting parser.</returns>
	public static TextParser<R> Select<T1, T2, T3, T4, R>(this TextParser<(T1, T2, T3, T4)> parser, Func<T1, T2, T3, T4, R> selector)
	{
		return parser.Select(x => selector(x.Item1, x.Item2, x.Item3, x.Item4));
	}

	public static TextParser<T[][]> MapJagged<T>(this TextParser<T> parser)
	{
		return parser.Many().Lines();
	}

	public static TextParser<T[,]> Map<T>(this TextParser<T> parser)
	{
		return parser.MapJagged()
			.Select(x =>
			{
				if (x.Length == 0)
					return new T[0, 0];
				var map = new T[x.Length, x[0].Length];
				for (var i = 0; i < map.GetLength(0); i++)
				for (var j = 0; j < map.GetLength(1); j++)
					map[i, j] = x[i][j];
				return map;
			});
	}
}