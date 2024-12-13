using System.Text.RegularExpressions;

namespace aoc.Year2024;

internal partial class Day03
{
	internal partial class Part1
	{
		private readonly Example example = new(
			"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))",
			161
		);

		public long Solve(string input)
		{
			var regex = new Regex(@"mul\((?<operand1>\d+),(?<operand2>\d+)\)");
			return regex.Matches(input).Sum(m =>
				int.Parse(m.Groups["operand1"].Value)
				* int.Parse(m.Groups["operand2"].Value)
			);
		}
	}

	internal partial class Part2
	{
		private readonly Example example = new(
			"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))",
			48
		);

		public long Solve(string input)
		{
			var regex = new Regex(@"do\(\)|don't\(\)|mul\((?<operand1>\d+),(?<operand2>\d+)\)");

			return regex.Matches(input)
				.Aggregate((enabled: true, sum: 0),
					(x, match) => match.Value switch
					{
						"do()" => (true, x.sum),
						"don't()" => (false, x.sum),
						_ => x.enabled
							? (true, x.sum + int.Parse(match.Groups["operand1"].Value) * int.Parse(match.Groups["operand2"].Value))
							: x
					}).sum;
		}
	}
}