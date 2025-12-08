using Common.Search;

namespace aoc.Year2024;

internal partial class Day16
{
	private readonly Example example = new(
		"""
		###############
		#.......#....E#
		#.#.###.#.###.#
		#.....#.#...#.#
		#.###.#####.#.#
		#.#.#.......#.#
		#.#.#####.###.#
		#...........#.#
		###.#.#####.#.#
		#...#.....#.#.#
		#.#.#.###.#.#.#
		#.....#...#.#.#
		#.###.#.#.#.#.#
		#S..#.....#...#
		###############
		""");

	private readonly Example example1 = new(
		"""
		#################
		#...#...#...#..E#
		#.#.#.#.#.#.#.#.#
		#.#.#.#...#...#.#
		#.#.#.#.###.#.#.#
		#...#.#.#.....#.#
		#.#.#.#.#.#####.#
		#.#...#.#.#.....#
		#.#.#####.#.###.#
		#.#.#.......#...#
		#.#.###.#####.###
		#.#.#...#.....#.#
		#.#.#.#####.###.#
		#.#.#.........#.#
		#.#.#.#########.#
		#S#.............#
		#################
		""");

	public M<char> Parse(string input)
	{
		return Character.AnyChar.Map().Parse(input);
	}

	IEnumerable<((V<int> point, V<int> direction) newState, int weight)> Adjacency((V<int> point, V<int> direction) arg, M<char> input)
	{
		yield return ((arg.point, arg.direction.RotateCw()), 1000);
		yield return ((arg.point, arg.direction.RotateCcw()), 1000);

		if (input[arg.point] != '#')
			yield return ((arg.point + arg.direction, arg.direction), 1);
	}
	
	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 7036);
			Expect(example1, 11048);
		}

		public int Solve(M<char> input)
		{
			var start = input.AsEnumerable().Single(x => x.element == 'S');
			var finish = input.AsEnumerable().Single(x => x.element == 'E');

			return Dijkstra.StartWith((start.point, direction: Directions.E))
				.WithAdjacency(arg => Adjacency(arg, input))
				.FindTarget(state => state.point == finish.point)
				.Len;
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 45);
			Expect(example1, 64);
		}

		public int Solve(M<char> input)
		{
			var start = input.AsEnumerable().Single(x => x.element == 'S');
			var finish = input.AsEnumerable().Single(x => x.element == 'E');

			return Dijkstra.StartWith((start.point, direction: Directions.E))
				.WithAdjacency(x => Adjacency(x, input))
				.AllShortestPaths()
				.FindTarget(x => x.point == finish.point)
				.SelectMany(path => path.PathList)
				.Select(item => item.Item.point)
				.Distinct()
				.Count();
		}
	}
}