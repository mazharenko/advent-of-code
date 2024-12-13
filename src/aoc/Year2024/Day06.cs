using aoc.Common;
using MoreLinq;
using V = aoc.Common.V<int>;

namespace aoc.Year2024;

internal partial class Day06
{
	private readonly Example example = new(
		"""
		....#.....
		.........#
		..........
		..#.......
		.......#..
		..........
		.#..^.....
		........#.
		#.........
		......#...
		""");

	public char[,] Parse(string input)
	{
		return Character.In('.', '#', '^').Map().Parse(input);
	}

	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 41);
		}
		
		public int Solve(char[,] input)
		{
			var (guardPosition, _) = input.AsEnumerable().Single(x => x.element == '^');

			return WalkUntilOutside(input, guardPosition, Directions.Up)
				.Select(x => x.pos)
				.Distinct()
				.Count();
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 6);
		}

		public int Solve(char[,] input)
		{
			var (guardPosition, _) = input.AsEnumerable().Single(x => x.element == '^');
			var originalVisited =
				WalkUntilOutside(input, guardPosition, Directions.Up)
					.Select(x => x.pos).Distinct();
			return originalVisited.Count(obstacleCandidate =>
			{
				var copy = input.Map((p, cell) => p == obstacleCandidate ? '#' : cell);
				return
					WalkUntilOutside(copy, guardPosition, Directions.Up)
						// enumerable returned will sometimes be infinite and looped.
						// as soon as we discover a duplicate, we go continue to the next obstacle candidate.
						// Duplicates is documented to be deferred and not populate the whole sequence.
						.Duplicates().Any();
			});
		}
	}

	private static IEnumerable<(V pos, V dir)> WalkUntilOutside(char[,] map, V pos, V dir)
	{
		while (true)
		{
			yield return (pos, dir);
			if (!map.TryAt(pos + dir, out var nextCell)) break;
			if (nextCell is '#')
				dir = dir.RotateCw();
			else
				pos += dir;
		}
	}
}