using aoc.Common;
using aoc.Common.Search;
using MoreLinq;

namespace aoc.Year2024;

internal partial class Day10
{
	private readonly Example example = new(
		"""
		89010123
		78121874
		87430965
		96549874
		45678903
		32019012
		01329801
		10456732
		""");

	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 36);
		}

		public int Solve(int[,] input)
		{
			var trailHeads = input.AsEnumerable().Where(x => x.element == 0).ToArray();

			return trailHeads.Sum(head =>
			{
				return 
					Dijkstra.StartWith(head.point)
						.WithAdjacency(new MapAdjacency(input))
						.Items()
						.Where(p => input.At(p) == 9)
						.Distinct()
						.Count();
			});
		}

		public int[,] Parse(string input)
		{
			return Character.Digit.Select(c => int.Parse(c.ToString()))
				.Map().Parse(input);
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 81);
		}

		public int Solve(int[,] input)
		{
			var trailHeads = input.AsEnumerable().Where(x => x.element == 0).ToArray();

			return trailHeads.Sum(head =>
			{
				return Dijkstra
					.StartWith(head.point)
					// the Adjacency guarantees that the graph search won't blow up, but we need all the paths
					//.WithoutTrackingVisited()
					.WithAdjacency(new MapAdjacency(input))
					.WithoutRelaxation()
					.Items()
					.Count(p => input.At(p) == 9);
			});
		}

		public int[,] Parse(string input)
		{
			return Character.Digit.Select(c => int.Parse(c.ToString()))
				.Map().Parse(input);
		}
	}

	private class MapAdjacency(int[,] map) : IAdjacency<V<int>>
	{
		public IEnumerable<(V<int> newState, int weight)> GetAdjacent(V<int> pos)
		{
			// todo: special bfs case for maps 
			return Directions.All4().Select(d => d + pos)
				.Where(map.Inside)
				.Where(adj => map.At(adj) == map.At(pos) + 1) // slope
				.Select(p => (p, 1));
		}
	}
}