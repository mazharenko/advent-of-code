using Microsoft.Z3;
using Superpower.Model;

namespace aoc.Year2025;

internal partial class Day10
{
	public record Light(bool OnRequirement, int JoltageRequirement, int[] ButtonIds);

	public record InitProcedure(Light[] Lights);


	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 7);
		}

		public int Solve(InitProcedure[] input)
		{
			var maxButtonId = input.SelectMany(pr => pr.Lights).SelectMany(l => l.ButtonIds).Max();

			return input.Sum(initProcedure =>
				{
					using var ctx = new Context();
					var solver = ctx.MkOptimize();
					var buttonPresses = (..(maxButtonId + 1)).AsEnumerable()
						.Select(i => ctx.MkIntConst($"button_{i}_presses")).ToArray<ArithExpr>();

					solver.Add(ctx.MkAnd(buttonPresses.Select(presses => presses >= 0)));

					foreach (var light in initProcedure.Lights)
					{
						solver.Add(
							ctx.MkEq(
								ctx.MkMod(
									(IntExpr)ctx.MkAdd(light.ButtonIds.Select(buttonId => buttonPresses[buttonId])),
									ctx.MkInt(2)
								),
								ctx.MkInt(light.OnRequirement ? 1 : 0)
							)
						);
					}

					var totalButtonPresses = ctx.MkAdd(buttonPresses);
					var minimize = solver.MkMinimize(totalButtonPresses);

					if (solver.Check() != Status.SATISFIABLE)
						throw new Exception("unsatisfiable");

					return ((IntNum)minimize.Lower).Int;
				}
			);
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 33);
		}

		public int Solve(InitProcedure[] input)
		{
			var maxButtonId = input.SelectMany(pr => pr.Lights).SelectMany(l => l.ButtonIds).Max();
			using var ctx = new Context();
			var solver = ctx.MkOptimize();

			var buttonPresses = (..(maxButtonId + 1)).AsEnumerable()
				.Select(i => ctx.MkIntConst($"button_{i}_presses")).ToArray<ArithExpr>();
			solver.Add(ctx.MkAnd(buttonPresses.Select(presses => presses >= 0)));

			return input.Sum(initProcedure =>
				{
					solver.Push();

					foreach (var light in initProcedure.Lights)
					{
						solver.Add(
							ctx.MkEq(
								ctx.MkAdd(light.ButtonIds.Select(buttonId => buttonPresses[buttonId])),
								ctx.MkInt(light.JoltageRequirement)
							)
						);
					}

					var totalButtonPresses = ctx.MkAdd(buttonPresses);
					var minimize = solver.MkMinimize(totalButtonPresses);

					if (solver.Check() != Status.SATISFIABLE)
						throw new Exception("unsatisfiable");
					var result = ((IntNum)minimize.Lower).Int;
					solver.Pop();
					return result;
				}
			);
		}
	}

	private readonly Example example = new(
		"""
		[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
		[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
		[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
		""");

	public InitProcedure[] Parse(string input)
	{
		var targetParser = Character.In('.', '#').Many()
			.Select(ar => ar.Select(c => c == '#').ToArray());

		var buttonParser =
			Numerics.IntegerInt32.ManyDelimitedBy(Character.EqualTo(','))
				.Between(Character.EqualTo('('), Character.EqualTo(')'))
				.ManyDelimitedBySpaces();

		var joltageParser =
			Numerics.IntegerInt32.ManyDelimitedBy(Character.EqualTo(','));

		var parser = Template.Matching<bool[], int[][], int[]>($"[{targetParser}] {buttonParser} {{{joltageParser}}}")
			.Select((lightsDiagram, buttons, joltage) =>
			{
				var lights = lightsDiagram.Select((l, i) =>
				{
					return new Light(l, joltage[i], buttons.Index().Where(b => b.Item.Contains(i)).Select(b => b.Index).ToArray());
				}).ToArray();
				return new InitProcedure(lights);
			});

		return parser.Lines().Parse(input);
	}
}