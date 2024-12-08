namespace aoc.Year2024;

internal partial class Day07
{
	public record Equation(long Result, long[] Operands);

	private bool CanBeTrue(Equation equation, Func<long, long, long>[] operators)
	{
		return CanBeTrue(equation.Result, equation.Operands[0], equation.Operands.AsSpan()[1..], operators);
	}

	private bool CanBeTrue(long expectedResult, long accResult, ReadOnlySpan<long> operands, Func<long, long, long>[] operators)
	{
		if (operands.Length == 0)
			return expectedResult == accResult;
		var head = operands[0];
		var rest = operands[1..];
		foreach (var op in operators)
			if (CanBeTrue(expectedResult, op(accResult, head), rest, operators))
				return true;
		return false;
	}

	public Equation[] Parse(string input)
	{
		return
			Numerics.IntegerInt64.ThenIgnore(Span.EqualTo(": "))
				.Then(Numerics.IntegerInt64.ManyDelimitedBySpaces())
				.Select((res, operands) => new Equation(res, operands))
				.Lines().Parse(input);
	}

	internal partial class Part1
	{
		private readonly Example example = new(
			"""
			190: 10 19
			3267: 81 40 27
			83: 17 5
			156: 15 6
			7290: 6 8 6 15
			161011: 16 10 13
			192: 17 8 14
			21037: 9 7 18 13
			292: 11 6 16 20
			""", 3749);

		public long Solve(Equation[] equations)
		{
			var operators = new Func<long, long, long>[] { (x, y) => x + y, (x, y) => x * y };
			return equations
				.Where(eq => CanBeTrue(eq, operators)).Sum(equation => equation.Result);
		}
	}

	internal partial class Part2
	{
		private readonly Example example = new(
			"""
			190: 10 19
			3267: 81 40 27
			83: 17 5
			156: 15 6
			7290: 6 8 6 15
			161011: 16 10 13
			192: 17 8 14
			21037: 9 7 18 13
			292: 11 6 16 20
			""", 11387);

		public long Solve(Equation[] equations)
		{
			var operators = new Func<long, long, long>[]
			{
				(x, y) => x + y,
				(x, y) => x * y,
				(x, y) => long.Parse($"{x}{y}")
			};
			return equations
				.Where(eq => CanBeTrue(eq, operators)).Sum(equation => equation.Result);
		}
	}
}