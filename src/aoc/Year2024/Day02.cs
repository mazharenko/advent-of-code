using MoreLinq;

namespace aoc.Year2024;

internal partial class Day02
{
	private readonly Example example = new(
		"""
		7 6 4 2 1
		1 2 7 8 9
		9 7 6 2 1
		1 3 2 4 5
		8 6 4 4 1
		1 3 6 7 9
		""");
	
	private static bool Safe(IEnumerable<long> levels)
	{
		var levelChanges = levels.Pairwise((x, y) => x - y).ToArray();
		return levelChanges.All(change => change is >= 1 and <= 3)
		       || levelChanges.All(change => change is >= -3 and <= -1);
	}

	public long[][] Parse(string input)
	{
		var parser = Numerics.IntegerInt64.ManyDelimitedBySpaces()
			.Lines();

		return parser.Parse(input);
	}

	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 2);
		}

		public int Solve(long[][] input)
		{
			return input.Count(Safe);
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 4);
		}

		public int Solve(long[][] input)
		{
			return input.Count(levels =>
			{
				return Safe(levels)
				       || (..levels.Length).AsEnumerable().Any(i =>
					       Safe([..levels[..i], ..levels[(i + 1)..]])
				       );
			});
		}
	}
}