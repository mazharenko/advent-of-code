using Superpower.Model;

// ReSharper disable once CheckNamespace
namespace Superpower.Parsers;

public static class SpanX
{
	public static TextParser<Unit> EndOfInput { get; } = input =>
	{
		if (input.IsAtEnd)
			return Result.Value(Unit.Value, input, input);
		return Result.Empty<Unit>(input, ["end of input"]);
	};
		
	/// <summary>
	/// Parse until a character not equal to SPACE (U+0020) is encountered, returning the matched span of whitespace.
	/// </summary>
	/// <remarks>
	/// Requires at least one whitespace character.
	/// </remarks>
	public static TextParser<TextSpan> Space { get; } = input =>
	{
		var next = input.ConsumeChar();
		while (next is { HasValue: true, Value: ' ' }) 
			next = next.Remainder.ConsumeChar();

		return next.Location == input 
			? Result.Empty<TextSpan>(input) 
			: Result.Value(input.Until(next.Location), input, next.Location);
	};

	/// <summary>
	/// Parse "\n" OR "\r\n", returning the matched span.
	/// </summary>
	public static TextParser<TextSpan> NewLine { get; } = 
		Span.EqualTo("\r\n").Or(Span.EqualTo("\n")).Named("New line");

	public static TextParser<TextSpan> BlockSeparator =>
		NewLine.Then(NewLine).Select((nl1, nl2)
			=> new TextSpan(nl1.Source!, nl1.Position, nl1.Length + nl2.Length)
		).Named("Block separator");
		
	
	/// <summary>
	/// Parse input until the <paramref name="text"/> string is present. <paramref name="text"/> is then skipped.
	/// </summary>
	/// <param name="text">The string to match until. The content of the <paramref name="text"/> is not included in the result.</param>
	/// <returns>A parser that will match anything until the <paramref name="text"/> value is found or end-of-input is reached.</returns>
	/// <exception cref="ArgumentNullException">The <paramref name="text"/> is null.</exception>
	public static TextParser<TextSpan> ExceptSkip(string text) => ExceptSkip(text, StringComparison.Ordinal);

	/// <summary>
	/// Parse input until the <paramref name="text"/> string is present, ignoring character case. <paramref name="text"/> is then skipped.
	/// </summary>
	/// <param name="text">The string to match until. The content of the <paramref name="text"/> is not included in the result.</param>
	/// <returns>A parser that will match anything until the <paramref name="text"/> value is found or end-of-input is reached.</returns>
	/// <exception cref="ArgumentNullException">The <paramref name="text"/> is null.</exception>
	public static TextParser<TextSpan> ExceptIgnoreCaseSkip(string text) => ExceptSkip(text, StringComparison.OrdinalIgnoreCase);

	private static TextParser<TextSpan> ExceptSkip(string text, StringComparison comparison)
	{
		if (text == null) throw new ArgumentNullException(nameof(text));
		if (text.Length == 0) throw new ArgumentOutOfRangeException(nameof(text), "A non-empty string is required.");

		var expectations = new[] { $"a non-empty span without `{text}`" };

		return input =>
		{
			if (input.Length == 0)
				return Result.Empty<TextSpan>(input, expectations);
			var matchIndex = input.Source!.IndexOf(text, input.Position.Absolute, comparison);
			if (matchIndex == input.Position.Absolute)
				return Result.Empty<TextSpan>(input.Skip(text.Length), expectations);
			if (matchIndex == -1)
				return Result.Value(input.First(input.Length), input, input.Skip(input.Length));
			var matchLength = matchIndex - input.Position.Absolute;
			var remainder = input.Skip(matchLength + text.Length);
			return Result.Value(input.First(matchLength), input, remainder);
		};
	}
}
