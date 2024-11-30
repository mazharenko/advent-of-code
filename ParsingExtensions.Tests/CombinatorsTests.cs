namespace ParsingExtensions.Tests;

public class CombinatorsTests
{
	[Test]
	public void Lines()
	{
		const string source = "2\n3\n4\r\n1";
		int[] expected = [2, 3, 4, 1];
		var parser = Numerics.IntegerInt32.Lines();
		var result = parser.TryParse(source);

		result.HasValue.Should().BeTrue();
		result.Value.Should().BeEquivalentTo(expected);
	}

	[Test]
	public void Blocks()
	{
		const string source = "2\n\n3\r\n\n4\n\r\n1";
		int[] expected = [2, 3, 4, 1];
		var parser = Numerics.IntegerInt32.Blocks();
		var result = parser.TryParse(source);

		result.HasValue.Should().BeTrue();
		result.Value.Should().BeEquivalentTo(expected);
	}

	[Test]
	public void LinesInBlocks()
	{
		const string source = "2\n3\n\n4\n1";
		int[][] expected = [[2, 3], [4, 1]];
	
		var parser = Numerics.IntegerInt32.Lines().Blocks();
		var result = parser.TryParse(source);

		result.HasValue.Should().BeTrue();
		result.Value.Should().BeEquivalentTo(expected);
	}

	[Test]
	public void ThenBlock()
	{
		const string source = "2\n\n4\n1\n\n3";
		var expected = ((2, new [] {4, 1}), 3);

		var parser = Numerics.IntegerInt32.Block()
			.ThenBlock(Numerics.IntegerInt32.Lines())
			.ThenBlock(Numerics.IntegerInt32);
		var result = parser.TryParse(source);
		
		result.HasValue.Should().BeTrue();
		result.Value.Should().BeEquivalentTo(expected);
	}

	[Test]
	public void MapJagged()
	{
		const string source = ".=.\n+.+";
		char[][] expected =
		[
			".=.".ToCharArray(),
			"+.+".ToCharArray()
		];
		
		var parser = Character.Except('\n').MapJagged();
		var result = parser.TryParse(source);
		
		result.HasValue.Should().BeTrue();
		result.Value.Should().BeEquivalentTo(expected);
	}

	[Test]
	public void Map()
	{
		const string source = ".=.\n+.+";
		var expected = new[,]
		{
			{ '.', '=', '.' },
			{ '+', '.', '+' }
		};

		var parser = Character.Except('\n').Map();
		var result = parser.TryParse(source);

		result.HasValue.Should().BeTrue();
		result.Value.Should().BeEquivalentTo(expected);
	}
}