using aoc.Common;
using MoreLinq;

namespace aoc.Year2025;

internal partial class Day04
{
	public char[,] Parse(string input)
	{
		return Character.AnyChar.Map().Parse(input);
	}

	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 13);
		}

		public int Solve(char[,] input)
		{
			return input.AsEnumerable()
				.Where(x => x.element == '@')
				.Count(x =>
					{
						return Directions.All8().Select(d => d + x.point)
							.Count(a => input.TryAt(a) == '@') < 4;
					}
				);
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 43);
		}

		private static int TryRemoveRolls(char[,] map, V<int>[] rollPoints)
		{
			// on each step only the rolls around those removed on the previous step are worth trying
			var removable = rollPoints
				.Where(point =>
					map.TryAt(point) == '@'
					&& Directions.All8().Select(d => d + point)
						.Count(a => map.TryAt(a) == '@') < 4)
				.ToArray();
			
			removable.ForEach(point => map.Set(point, '.'));
			
			return removable.Length 
			       + removable.Sum(point => TryRemoveRolls(map, Directions.All8().Select(d => d + point).ToArray()));
		}

		public int Solve(char[,] input)
		{
			return TryRemoveRolls(input, input.AsEnumerable().Select(x => x.point).ToArray());
		}
	}
	
	private readonly Example example = new(
		"""
		..@@.@@@@.
		@@@.@.@.@@
		@@@@@.@.@@
		@.@@@@..@.
		@@.@@@@.@@
		.@@@@@@@.@
		.@.@.@.@@@
		@.@@@.@@@@
		.@@@@@@@@.
		@.@.@@@.@.
		"""
	);
}