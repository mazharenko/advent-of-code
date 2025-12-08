using Common.Search;
using MoreLinq;

namespace aoc.Year2024;

internal partial class Day12
{
	private readonly Example example = new(
		"""
		RRRRIICCFF
		RRRRIICCCF
		VVRRRCCFFF
		VVRCCCJFFF
		VVVVCJJCFE
		VVIVCCJJEE
		VVIIICJJEE
		MIIIIIJJEE
		MIIISIJEEE
		MMMISSJEEE
		""");

	public M<char> Parse(string input)
	{
		return Character.Letter.Map().Parse(input);
	}

	internal partial class Part1
	{
		public int Solve(M<char> input)
		{
			var remainingPlots = input.AsEnumerable().Select(x => x.point).ToHashSet();

			var price = 0;
			while (remainingPlots.Count != 0)
			{
				var start = remainingPlots.First();
				// While traversing the graph, for each visited node, we not only determine its adjacent nodes 
				// but also count how many of them lie outside this region.
				var region = Dijkstra.StartWith((plot: start, plant: input[start], sides: (int?)null))
					.WithAdjacency(p =>
					{
						var dirs = Directions.All4();
						var allAdj = dirs.Select(d => d + p.plot).ToList();
						var (inside, outside) =
							allAdj.Partition(adj => input.TryAt(adj, out var adjValue) && adjValue == p.plant);

						return inside.Select(x => (x, input[x], (int?)null))
							.Append((p.plot, p.plant, outside.Count()));
					}).Items()
					.Where(x => x.sides is not null)
					.Select(x => (x.plot, sides: x.sides!.Value))
					.ToList();

				var area = region.Select(x => x.plot).Count();
				var perimeter = region.Sum(x => x.sides);

				price += area * perimeter;

				remainingPlots.ExceptWith(region.Select(x => x.plot));
			}

			return price;
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 1206);
		}

		private readonly Example example1 = new(
			"""
			EEEEE
			EXXXX
			EEEEE
			EXXXX
			EEEEE
			""", 236);

		private readonly Example example2 = new(
			"""
			AAAAAA
			AAABBA
			AAABBA
			ABBAAA
			ABBAAA
			AAAAAA
			""", 368);

		private readonly Example example3 = new(
			"""
			AAAA
			BBCD
			BBCC
			EEEC
			""", 80);

		public int Solve(M<char> input)
		{
			var remainingPoints = input.AsEnumerable().ToHashSet();

			var price = 0;
			while (remainingPoints.Count != 0)
			{
				var start = remainingPoints.First().point;
				var region = Dijkstra.StartWith(start)
					.WithAdjacency(p =>
					{
						var dirs = Directions.All4();

						return dirs.Select(d => d + p)
							.Where(adj => input.TryAt(adj, out var adjValue)
							              && adjValue == input[p]);
					})
					.Items().ToList();

				var area = region.Count;
				var extendedRegion =
					region.SelectMany(x => Directions.All8().Append(V<int>.Zero).Select(d => d + x))
						.Distinct().ToList();
				// Counting corners instead of sides
				// Scanning the region with a 2x2 window to determine how many corners are in it by counting the plots.
				var corners = extendedRegion
					.Sum(x =>
					{
						// 
						//  X .
						//  . .
						// 
						var blockToFindCorner = new[] { x, Directions.W + x, Directions.S + x, Directions.SW + x };
						var pointsFoundInBlock = blockToFindCorner.Where(l => region.Contains(l)).ToList();
						return pointsFoundInBlock switch
						{
							//   A .   . .   . .   . A   A A   A .   . A   A A     
							//   . .   A .   . A   . .   A .   A A   A A   . A     
							{ Count: 1 or 3 } => 1,
							//  A .    . A 
							//  . A    A . 
							[var found1, var found2] when found1.MDist(found2) == 2 => 2,
							//  A A   A A   . A   . .   A .   . . 
							//  A A   . .   . A   A A   A .   . . 
							_ => 0
						};
					});

				var sides = corners;
				price += area * sides;

				remainingPoints = remainingPoints.ExceptBy(region, x => x.point).ToHashSet();
			}

			return price;
		}
	}
}