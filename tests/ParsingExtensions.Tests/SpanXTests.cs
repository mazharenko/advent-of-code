namespace ParsingExtensions.Tests;

public class SpanXTests
{
	[Test]
	public void ExceptSkipWithSpaceButSpaceSeparators()
	{
		const string source = "1 2 3 | 4 5 6";
		(int[], int[]) expected = ([1, 2, 3], [4, 5, 6]);
		
		var parser =
			SpanX.ExceptSkip(" | ")
				.Apply(Numerics.IntegerInt32.ManyDelimitedBySpaces())
				.Then(Numerics.IntegerInt32.ManyDelimitedBySpaces());
		var result = parser.TryParse(source);
		
		result.HasValue.Should().BeTrue();
		result.Value.Should().BeEquivalentTo(expected);
	}
}