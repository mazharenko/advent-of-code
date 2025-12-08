using mazharenko.AoCAgent.Generator;

namespace aoc.Year2024;

internal partial class Day11
{
	internal partial class Part1
	{
		private readonly Example example = new(
			"""
			125 17
			""", 55312L);

		public long Solve(long[] input)
		{
			var countFunc = Count;
			var countM = countFunc.MemoizeRec();
			return input.Sum(x => countM(x, 25));
		}

		public long[] Parse(string input)
		{
			return Numerics.IntegerInt64.ManyDelimitedBySpaces().Parse(input);
		}
	}

	[BypassNoExamples]
	internal partial class Part2
	{
		public long Solve(long[] input)
		{
			var countFunc = Count;
			var countM = countFunc.MemoizeRec();
			return input.Sum(x => countM(x, 75));
		}

		public long[] Parse(string input)
		{
			return Numerics.IntegerInt64.ManyDelimitedBySpaces().Parse(input);
		}
	}

	private static long Count(Func<long, int, long> countFunc, long n, int t)
	{
		if (t == 0)
			return 1;
		if (n == 0)
			return countFunc(1, t - 1);

		if (n.ToString().Length % 2 == 0)
		{
			var s = n.ToString();
			return countFunc(long.Parse(s[..(s.Length / 2)]), t - 1)
			       + countFunc(long.Parse(s[(s.Length / 2)..]), t - 1);
		}

		return countFunc(n * 2024, t - 1);
	}
}