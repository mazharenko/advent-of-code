using aoc.Common;

namespace aoc.Year2025;

internal partial class Day03
{
	private readonly Example example = new(
		"""
		234234234234278
		818181911112111
		"""
	);
	private long MaxJoltage(int[] bank, int count)
	{
		var dp = new long[count + 1];
		foreach (var battery in bank)
		{
			for (var i = count; i > 0; i--)
			{
				var prev = dp[i - 1];
				dp[i] = Math.Max(dp[i], prev * 10 + battery);
			}
		}

		return dp[count];
	}
	
	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 357);
		}

		public long Solve(int[][] input) => input.Sum(bank => MaxJoltage(bank, 2));
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 3121910778619);
		}

		public long Solve(int[][] input) => input.Sum(bank => MaxJoltage(bank, 12));
	}
	
	public int[][] Parse(string input)
	{
		var parser = Character.Digit.Select(c => c.ToInteger()).MapJagged();
		return parser.Parse(input);
	}

}