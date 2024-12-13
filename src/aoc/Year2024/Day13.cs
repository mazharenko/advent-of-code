using aoc.Common;
using Microsoft.Z3;

namespace aoc.Year2024;

internal partial class Day13
{
	private readonly Example example = new(
		"""
		Button A: X+94, Y+34
		Button B: X+22, Y+67
		Prize: X=8400, Y=5400

		Button A: X+26, Y+66
		Button B: X+67, Y+21
		Prize: X=12748, Y=12176

		Button A: X+17, Y+86
		Button B: X+84, Y+37
		Prize: X=7870, Y=6450

		Button A: X+69, Y+23
		Button B: X+27, Y+71
		Prize: X=18641, Y=10279
		""");

	public (V<int> a, V<int> b, V<long>prize)[] Parse(string input)
	{
		var parser =
			Span.EqualTo("Button A: X+")
				.IgnoreThen(Numerics.IntegerInt32)
				.ThenIgnore(Span.EqualTo(", Y+"))
				.Then(Numerics.IntegerInt32)
				.Select(V.Create)
				.ThenLine(
					Span.EqualTo("Button B: X+")
						.IgnoreThen(Numerics.IntegerInt32)
						.ThenIgnore(Span.EqualTo(", Y+"))
						.Then(Numerics.IntegerInt32)
						.Select(V.Create)
				)
				.ThenLine(Span.EqualTo("Prize: X=")
					.IgnoreThen(Numerics.IntegerInt64)
					.ThenIgnore(Span.EqualTo(", Y="))
					.Then(Numerics.IntegerInt64)
					.Select(V.Create))
				.Select(x =>
				{
					var ((a, b), prize) = x;
					return (a, b, prize);
				});

		return parser.Blocks().Parse(input);
	}

	private long SolveBase((V<int> a, V<int> b, V<long> prize)[] input)
	{
		using var ctx = new Context();
		var solver = ctx.MkOptimize();
		var pressesA = ctx.MkIntConst("presses_a");
		var pressesB = ctx.MkIntConst("presses_b");

		solver.Add(pressesA >= 0 & pressesB >= 0);
		return input.Sum(
			machine =>
			{
				solver.Push();
				solver.Add(ctx.MkEq(ctx.MkInt(machine.prize.X),
					pressesA * machine.a.X + pressesB * machine.b.X));
				solver.Add(ctx.MkEq(ctx.MkInt(machine.prize.Y),
					pressesA * machine.a.Y + pressesB * machine.b.Y));

				var minimize = solver.MkMinimize(3 * pressesA + pressesB);

				var res =  solver.Check() == Status.SATISFIABLE ? ((IntNum)minimize.Lower).Int64 : 0L;
				solver.Pop();
				return res;
			}
		);
	}

	internal partial class Part1
	{
		public long Solve((V<int> a, V<int> b, V<long> prize)[] input) => SolveBase(input);
	}

	internal partial class Part2
	{
		public long Solve((V<int> a, V<int> b, V<long> prize)[] input)
		{
			return SolveBase(input.Select(machine 
				=> (machine.a, machine.b, machine.prize + V<long>.One * 10000000000000L)
			).ToArray());
		}
	}
}
