namespace aoc.Year2024.Day01;

internal partial class Day01
{
	private static (int[] first, int[] second) ParseBase(string input)
	{
		var parser =
			Numerics.IntegerInt32.ThenIgnore(SpanX.Space)
				.Then(Numerics.IntegerInt32)
				.Lines()
				.Select(lines => (
					lines.Select(x => x.Item1).ToArray(),
					lines.Select(x => x.Item2).ToArray())
				);

		return parser.Parse(input);
	}

	internal partial class Part1
	{
		private readonly Example example = new(
			"""
			3   4
			4   3
			2   5
			1   3
			3   9
			3   3
			""", 11);

		public (int[] first, int[] second) Parse(string input) => ParseBase(input);

		public int Solve((int[] first, int[] second) input)
		{
			var totalDifference =
				input.first.Order()
					.Zip(input.second.Order(), Distance).Sum();
			
			return totalDifference;
			
			int Distance(int x, int y) => Math.Abs(x - y);
		}
	}

	internal partial class Part2
	{
		private readonly Example example = new(
			"""
			3   4
			4   3
			2   5
			1   3
			3   9
			3   3
			""", 31);

		
		public (int[] first, int[] second) Parse(string input) => ParseBase(input);

		public int Solve((int[] first, int[] second) input)
		{
			var secondGroups = input.second.ToLookup(x => x);
				
			var similarityScore = input.first.Select(x => x * secondGroups[x].Count()).Sum();

			return similarityScore;
		}
	}
}