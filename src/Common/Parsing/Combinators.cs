using System.Text.RegularExpressions;
using Superpower.Model;
// ReSharper disable once CheckNamespace
using Superpower.Parsers;

// ReSharper disable once CheckNamespace
namespace Superpower;

public static partial class Combinators
{
	/// <param name="first">The first parser.</param>
	/// <typeparam name="T">The type of value being parsed by <paramref name="first" />.</typeparam>
	extension<T>(TextParser<T> first)
	{
		/// <summary>
		/// Construct a parser that applies <paramref name="first" />, applies <paramref name="second" /> and returns their results as a tuple.
		/// </summary>
		/// <typeparam name="U">The type of value being parsed by <paramref name="second" />.</typeparam>
		/// <param name="second">The second parser.</param>
		/// <returns>The resulting parser.</returns>
		public TextParser<(T, U)> Then<U>(TextParser<U> second)
		{
			return first.Then(t => second.Select(u => (t, u)));
		}

		/// <summary>
		/// Construct a parser that matches <paramref name="first"/> zero or more times, delimited by spaces.
		/// </summary>
		/// <returns>The resulting parser.</returns>
		public TextParser<T[]> ManyDelimitedBySpaces()
		{
			return first.ManyDelimitedBy(SpanX.Space);
		}

		/// <summary>
		/// Construct a parser that matches one or more instances of applying <paramref name="first"/>, delimited by space.
		/// </summary>
		/// <returns>The resulting parser.</returns>
		public TextParser<T[]> AtLeastOnceDelimitedBySpaces()
		{
			return first.AtLeastOnceDelimitedBy(SpanX.Space);
		}
	}

	[GeneratedRegex("^.*?(?=(\r\n|\n){2})", RegexOptions.Singleline)]
	private static partial Regex TextUntilBlockSeparatorRegex();
	
	[GeneratedRegex("^.*?(?=(\r\n|\n){1})", RegexOptions.Singleline)]
	private static partial Regex TextUntilLineSeparatorRegex();

	/// <param name="parser">The parser.</param>
	/// <typeparam name="T">The type of value being parsed.</typeparam>
	extension<T>(TextParser<T> parser)
	{
		public TextParser<T> Block()
		{
			TextParser<TextSpan> untilSeparatorParser = input =>
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

		private TextParser<T> Line()
		{
			TextParser<TextSpan> untilSeparatorParser = input =>
			{
				if (input.Length == 0)
					return Result.Empty<TextSpan>(input, ["some text until line separator"]);
				var m = TextUntilLineSeparatorRegex().Match(input.Source!, input.Position.Absolute, input.Length);
				if (!m.Success)
					return Result.Value(input, input, TextSpan.Empty);

				var remainder = input.Skip(m.Length);
				return Result.Value(input.First(m.Length), input, remainder);
			};

			return untilSeparatorParser.Apply(parser).ThenIgnore(SpanX.NewLine.Optional());
		}
		
		public TextParser<(T, U)> ThenBlock<U>(TextParser<U> second)
		{
			return parser.Then(second.Block());
		}

		public TextParser<U> ThenBlock<U>(Func<T, TextParser<U>> second)
		{
			return parser.Then(t => second(t).Block());
		}

		/// <summary>
		/// Construct a parser that matches <paramref name="parser" /> againts the trimmed input - without one trailing new line (\n or \r\n)
		/// </summary>
		/// <returns>The resulting parser.</returns>
		public TextParser<T> TrimTrailingNewLine()
		{
			// It's a bit tricky to ignore the trailing newline because when the Lines parser encounters it, it expects another line.
			// This can be resolved with tokenization, but we want to avoid that to keep things simple.
			return input =>
			{
				if (input.IsAtEnd)
					return parser(input);
				if (input.Source![input.Position.Absolute + input.Length - 1] == '\n')
				{
					var trimmedInput = new TextSpan(input.Source, input.Position, input.Length - 1);
					if (!trimmedInput.IsAtEnd &&
					    trimmedInput.Source![trimmedInput.Position.Absolute + trimmedInput.Length - 1] == '\r')
						trimmedInput = new TextSpan(trimmedInput.Source, trimmedInput.Position, trimmedInput.Length - 1);
					return parser(trimmedInput);
				}

				return parser(input);
			};
		}

		/// <summary>
		/// Construct a parser that matches <paramref name="parser" /> zero or more times, delimited by new lines and an optional trailing new line.
		/// </summary>
		/// <returns>The resulting parser.</returns>
		public TextParser<T[]> Lines()
		{
			return parser.Line().Many();
		}

		public TextParser<(T, U)> ThenLine<U>(TextParser<U> second)
		{
			return parser.ThenIgnore(SpanX.NewLine).Then(second);
		}

		public TextParser<U> ThenLine<U>(Func<T, TextParser<U>> second)
		{
			return parser.ThenIgnore(SpanX.NewLine).Then(second);
		}

		public TextParser<T> ThenIgnore<U>(TextParser<U> second)
		{
			return parser.Then(x => second.Select(_ => x));
		}

		public TextParser<T[]> Blocks()
		{
			return parser.Block().TrimTrailingNewLine().Many();
		}
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

	extension<T>(TextParser<T> parser)
	{
		public TextParser<T[][]> MapJagged()
		{
			return parser.Many().Lines();
		}

		public TextParser<T[,]> Map()
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
}