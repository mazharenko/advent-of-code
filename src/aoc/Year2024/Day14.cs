using mazharenko.AoCAgent.Generator;
using MoreLinq;

namespace aoc.Year2024;

internal partial class Day14
{
	public (V<int> p, V<int> v)[] Parse(string input)
	{
		return Template.Matching<(int py, int px, int vy, int vx)>(
				$"p={Numerics.IntegerInt32},{Numerics.IntegerInt32} v={Numerics.IntegerInt32},{Numerics.IntegerInt32}"
			)
			.Select(t => (V.Create(t.px, t.py), V.Create(t.vx, t.vy)))
			.Lines().Parse(input);
	}

	[BypassNoExamples]
	internal partial class Part1
	{
		public int Solve((V<int> p, V<int> v)[] input)
		{
			const int x = 103;
			const int y = 101;

			const int steps = 100;

			var newPositions = input.Select(guard =>
			{
				var newPosition = guard.p + guard.v * steps;
				return V.Create(newPosition.X.EuclideanRemainder(x), newPosition.Y.EuclideanRemainder(y));
			}).ToList();

			var q1 = newPositions.Where(v => v is { X: < x / 2, Y: < y / 2 });
			var q2 = newPositions.Where(v => v is { X: >= x / 2 + 1, Y: < y / 2 });
			var q3 = newPositions.Where(v => v is { X: < x / 2, Y: >= y / 2 + 1 });
			var q4 = newPositions.Where(v => v is { X: >= x / 2 + 1, Y: >= y / 2 + 1 });
			return q1.Count() * q2.Count() * q3.Count() * q4.Count();
		}
	}

	[BypassNoExamples]
	internal partial class Part2
	{
		public int Solve((V<int> p, V<int> v)[] input)
		{
			const int x = 103;
			const int y = 101;
			var likelyPatterns =
				(1..10000).AsEnumerable()
				.Scan((steps: 0, guards: input), (t, i) =>
				{
					return (i, t.guards.Select(guard =>
						{
							var newPosition = guard.p + guard.v;
							return (
								V.Create(newPosition.X.EuclideanRemainder(x),
									newPosition.Y.EuclideanRemainder(y)),
								guard.v
							);
						}
					).ToArray());
				})
				.Where(t =>
				{
					// assume the Christmas tree has many triangles
					// #..
					// ##.
					// ###
					var unique = t.guards.Select(g => g.p).ToHashSet();
					var triangles = unique.Where(guard =>
						unique.IsSupersetOf([
							guard + Directions.SE, guard + Directions.S, guard + Directions.SW, 
							guard + Directions.W, guard + Directions.NW
						]) && !unique.Overlaps([
							guard + Directions.N, guard + Directions.NE, guard + Directions.E
						])
					);
					
					return triangles.Count() >= 10;
				});

			return likelyPatterns.First().steps;
		}
	}
}