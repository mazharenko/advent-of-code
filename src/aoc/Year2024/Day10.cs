using aoc.Common;
using aoc.Common.BfsImpl;

namespace aoc.Year2024;

internal partial class Day10
{	private readonly Example example = new(
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
				var bfs =
					Bfs.Common.StartWith(head.point)
						.WithAdjacency(new MapAdjacency(input))
						.WithFolder(L.Empty<V<int>>(), (acc, path) =>
						{
							if (input.At(path.PathList.Head.Item) == 9)
								return (acc.Prepend(path.PathList.Head.Item), TraversalResult.Continue);
							return (acc, TraversalResult.Continue);
						});

				return bfs.Run().Distinct().Count();
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
				var bfs =
					Bfs.Common.StartWith(head.point)
						.WithAdjacency(new MapAdjacency(input))
						// the Adjacency guarantees that the graph search won't blow up, but we need all the paths
						.WithoutTrackingVisited()
						.WithFolder(0, (acc, path) =>
						{
							if (input.At(path.PathList.Head.Item) == 9)
								return (acc + 1, TraversalResult.Continue);
							return (acc, TraversalResult.Continue);
						});

				return bfs.Run();
			});
		}

		public int[,] Parse(string input)
		{
			return Character.Digit.Select(c => int.Parse(c.ToString()))
				.Map().Parse(input);
		}
	}

	public class MapAdjacency(int[,] map) : Bfs.Common.IAdjacency<V<int>>
	{
		public IEnumerable<(V<int> newState, int weight)> GetAdjacent(V<int> pos)
		{// todo: special bfs case for maps 
			return Directions.All4().Select(d => d + pos)
				.Where(map.Inside)
				.Where(adj => map.At(adj) == map.At(pos) + 1) // slope
				.Select(p => (p, 1));
		}
	}
}