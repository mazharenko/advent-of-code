using mazharenko.AoCAgent.Generator;

namespace aoc.Year2024;

[BypassNoExamples]
internal partial class Day19
{
	public (string[] patterns, string[] designs) Parse(string input)
	{
		return Character.Letter.Many().Text()
			.ManyDelimitedBy(Span.EqualTo(", "))
			.Block()
			.ThenBlock(
				Character.Letter.Many().Text()
					.Lines()
			).Parse(input);
	}

	private static long GetWaysCounts(string[] patterns, string design)
	{
		// How many ways to build the 0..i substring of <paramref name="design"/> from <paramref name="patterns"/>
		var dp = new long[design.Length + 1];
		dp[0] = 1;
		for (var i = 1; i <= design.Length; i++)
		{
			dp[i] =
				patterns.Where(pattern =>
					design.AsSpan(0, i).EndsWith(pattern)
				).Sum(
					pattern => dp[i - pattern.Length]
				);
		}

		return dp[^1];
	}

	internal partial class Part1
	{
		public int Solve((string[] patterns, string[] designs) input)
		{
			return input.designs.Count(design => GetWaysCounts(input.patterns, design) > 0);
		}
	}

	internal partial class Part2
	{
		public long Solve((string[] patterns, string[] designs) input)
		{
			return input.designs.Sum(design => GetWaysCounts(input.patterns, design));
		}
	}
}