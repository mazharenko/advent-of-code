
namespace aoc.Year2025;

internal partial class Day07
{
	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 21);
		}

		public int Solve(char[,] input)
		{
			var start = input.AsEnumerable().First(x => x.element == 'S').point;
			
			var beamYs = new HashSet<int> { start.Y };
			var splits = 0;
			
			for (var i = start.X; i < input.Height(); i++)
			{
				foreach (var beamY in beamYs.ToList())
				{
					if (input[i, beamY] == '^')
					{
						beamYs.Remove(beamY);
						beamYs.Add(beamY - 1);
						beamYs.Add(beamY + 1);
						splits++;
					}
				}
			}

			return splits;
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 40);
		}

		// все-таки рекурсия, но только если получится иммутабельность. разветлители можно хэшсетом, а лучи координатами нормальными
		// ну есть же immutable dictionary
		public long Solve(char[,] input)
		{
			var start = input.AsEnumerable() .First(x => x.element == 'S').point;

			var beamYs = new long[input.Width()];
				
			beamYs[start.Y] ++;
			
			for (var i = start.X; i < input.Height(); i++)
			{
				for (var y = 0; y < beamYs.Length; y++)
				{
					if (input[i, y] == '^')
					{
						beamYs[y - 1] += beamYs[y];
						beamYs[y + 1] += beamYs[y];
						beamYs[y] = 0;
					}
				}
			}

			return beamYs.Sum();
			
		}
	}

	public char[,] Parse(string input)
	{
		return Character.AnyChar.Map().Parse(input);
	}
	
	private readonly Example example = new(
		"""
		.......S.......
		...............
		.......^.......
		...............
		......^.^......
		...............
		.....^.^.^.....
		...............
		....^.^...^....
		...............
		...^.^...^.^...
		...............
		..^...^.....^..
		...............
		.^.^.^.^.^...^.
		...............
		""");
}