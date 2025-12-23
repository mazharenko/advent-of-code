
namespace aoc.Year2025;

internal partial class Day07
{
	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 21);
		}

		public int Solve(M<char> input)
		{
			var start = input.AsEnumerable().First(x => x.element == 'S').point;
			
			var beamYs = new HashSet<int> { start.Y };
			var splits = 0;
			
			for (var i = start.X; i < input.Height; i++)
			{
				var newBeamYs = new HashSet<int>();
				foreach (var beamY in beamYs)
				{
					if (input[i, beamY] == '^')
					{
						newBeamYs.Add(beamY - 1);
						newBeamYs.Add(beamY + 1);
						splits++;
					}
					else newBeamYs.Add(beamY);
				}
				beamYs = newBeamYs;
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

		public long Solve(M<char> input)
		{
			var start = input.AsEnumerable() .First(x => x.element == 'S').point;

			var beamYs = new long[input.Width];
				
			beamYs[start.Y] ++;
			
			for (var i = start.X; i < input.Height; i++)
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

	public M<char> Parse(string input)
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