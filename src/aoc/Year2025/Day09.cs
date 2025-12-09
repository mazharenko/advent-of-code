using MoreLinq;
namespace aoc.Year2025;

internal partial class Day09
{
	public V<long>[] Parse(string input)
	{
		return Template.Matching<long, long>($"{Numerics.IntegerInt64},{Numerics.IntegerInt64}")
			.Select((a,b) => V.Create(b, a))
			.Lines().Parse(input);
	}

	private static long Area(V<long> a, V<long> b)
		=> (long.Abs(a.X - b.X) + 1) * (long.Abs(a.Y - b.Y) + 1);

	internal partial class Part1
	{
		public long Solve(V<long>[] input)
		{
			return input.UniquePairs(Area).Max();
		}
	}
	
	internal partial class Part2
	{
		private readonly Example example2 = new(
			"""
			11,7
			9,7
			9,5
			2,5
			2,4
			11,4
			""", 20);

		private readonly Example exampleCounterClockwise = new(
			"""
			11,4
			2,4
			2,5
			9,5
			9,7
			11,7
			""", 20);
		
		public Part2()
		{
			Expect(example, 24);
		}

		public long Solve(V<long>[] input)
		{
			var direction = input.Minima(p => p.X)
				.Take(2).Fold((a, b) => (b - a).Dir());

			var outers =
				input.Prepend(input[^1]).Append(input[0])
					.Window(3)
					.Choose(j =>
					{
						var prev = j[0];
						var current = j[1];
						var next = j[2];
						var prevDir = (current - prev).Dir();
						var nextDir = (next - current).Dir();

						if (direction == V.Create(0L, 1L)) // cw 
							if (nextDir == prevDir.RotateCw()) // convex
								return Maybe.Some(current - nextDir + prevDir);
							else if (nextDir.Dir() == prevDir.Dir().RotateCcw()) // concave
								return Maybe.Some(current + nextDir - prevDir);

						if (direction == V.Create(0L, -1L)) // ccw
							if (nextDir == prevDir.RotateCcw()) // convex
								return Maybe.Some(current - nextDir + prevDir);
							else if (nextDir == prevDir.RotateCw()) // concave
								return Maybe.Some(current + nextDir - prevDir);

						return Maybe.None;
					}).ToList();

			var outerBorders = outers.Append(outers[0])
				.Pairwise((a, b) => (a, b))
				.ToList();

			return input
				.UniquePairs()
				.Where(x =>
					!outerBorders.Any(border =>
						Math.Min(border.a.Y, border.b.Y) <= Math.Max(x.first.Y, x.second.Y)
						&& Math.Max(border.a.Y, border.b.Y) >= Math.Min(x.first.Y, x.second.Y)
						&& Math.Min(border.a.X, border.b.X) <= Math.Max(x.first.X, x.second.X)
						&& Math.Max(border.a.X, border.b.X) >= Math.Min(x.first.X, x.second.X)))
				.Select(x => Area(x.first, x.second)).Max();
		}
	}

	private readonly Example example = new(
		"""
		7,1
		11,1
		11,7
		9,7
		9,5
		2,5
		2,3
		7,3
		""");
}