using MoreLinq;

namespace aoc.Year2024;

internal partial class Day02
{
	private static bool Safe(IEnumerable<long> levels)
	{
		var levelChanges = levels.Pairwise((x, y) => x - y).ToArray();
		return levelChanges.All(change => change is >= 1 and <= 3)
		       || levelChanges.All(change => change is >= -3 and <= -1);
	}

	private static long[][] ParseLevels(string input)
	{
		var parser = Numerics.IntegerInt64.ManyDelimitedBySpaces()
			.Lines();

		return parser.Parse(input);
	}

	internal partial class Part1
	{
		private readonly Example example = new(
			"""
			7 6 4 2 1
			1 2 7 8 9
			9 7 6 2 1
			1 3 2 4 5
			8 6 4 4 1
			1 3 6 7 9
			""", 2);

		public int Solve(long[][] input)
		{
			return input.Count(Safe);
		}

		public long[][] Parse(string input) => ParseLevels(input);
	}

	internal partial class Part2
	{
		private readonly Example example = new(
			"""
			7 6 4 2 1
			1 2 7 8 9
			9 7 6 2 1
			1 3 2 4 5
			8 6 4 4 1
			1 3 6 7 9
			""", 4);

		public int Solve(long[][] input)
		{
			return input.Count(levels =>
			{
				return Safe(levels)
				       || Enumerable.Range(0, levels.Length).Any(i =>
					       Safe([..levels[..i], ..levels[(i + 1)..]])
				       );
			});
		}

		public long[][] Parse(string input) => ParseLevels(input);
	}
}