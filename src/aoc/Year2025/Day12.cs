using mazharenko.AoCAgent.Generator;

namespace aoc.Year2025;

internal partial class Day12
{
	[BypassNoExamples]
	internal partial class Part1
	{
		public (M<char>[] shapes, (int X, int Y, int[] quantity)[] regions) Parse(string input)
		{
			var parser = Template.Matching<int>($"{Numerics.IntegerInt32}:").Line()
				.IgnoreThen(Character.AnyChar.Map()).Block().Repeat(6)
				.ThenBlock(
					Template.Matching<int, int, int[]>(
							$"{Numerics.IntegerInt32}x{Numerics.IntegerInt32}: {Numerics.IntegerInt32.ManyDelimitedBySpaces()}")
						.Lines()
				);

			return parser.Parse(input);
		}


		public int Solve((M<char>[] shapes, (int X, int Y, int[] quantity)[] regions) input)
		{
			var shapeCounts = input.shapes.Select(m => m.AsEnumerable().Count(x => x.element == '#')).ToArray();
			if (!input.shapes.All(shape => shape is { Height: 3, Width: 3 }))
				throw new InvalidOperationException();
			
			return input.regions.Count(r =>
			{
				var definitelyDoesntFit = r.quantity.Zip(shapeCounts, (i, i1) => i * i1).Sum() > r.X * r.Y;
				var x = r.X - r.X % 3;
				var y = r.Y - r.Y % 3;
				var definitelyFits = r.quantity.Sum() <= x * y / 9;
				if (!(definitelyDoesntFit || definitelyFits))
					throw new InvalidOperationException();
				return definitelyFits;
			});
		}
	}

	internal partial class Part2
	{
		public string Solve(string input)
		{
			throw new NotImplementedException();
		}
	}
}