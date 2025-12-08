using static MoreLinq.Extensions.ScanExtension;

namespace aoc.Year2025;

internal partial class Day01
{
	internal enum Direction { L = -1, R = 1 }

	internal record Rotation(Direction Direction, int Distance)
	{
		public readonly int SignedDistance = (int)Direction * Distance;
	};


	private readonly Example example = new(
		"""
		L68
		L30
		R48
		L5
		R60
		L55
		L1
		L99
		R14
		L82
		""");
	
	public Rotation[] Parse(string input)
	{
		return Character.AnyChar.Then(Numerics.IntegerInt32)
			.Select((direction, distance) => 
				new Rotation(direction switch {'L' => Direction.L, 'R' => Direction.R}, distance)
			)
			.Lines()
			.Parse(input);
	}

	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 3);
		}

		public int Solve(Rotation[] input)
		{
			return input.Scan(50, (i, rotation) =>
					(i + rotation.SignedDistance).EuclideanRemainder(100)
				)
				.Count(x => x == 0);
		}
	}
	
	internal partial class Part2
	{
		private readonly Example example1 = new("R1000", 10);

		public Part2()
		{
			Expect(example, 6);
		}

		public int Solve(Rotation[] input)
		{
			return input.Aggregate((pos: 50, clicks: 0), (aggr, rotation) =>
			{
				var (pos, clicks) = aggr;

				var (fullRotations, distanceRest) = Math.DivRem(rotation.Distance, 100);

				var newPos = pos + distanceRest * (int)rotation.Direction;

				var additionalClicks = (pos, newPos) switch
				{
					(0, _) => 0,
					(_, <= 0) => 1,
					(_, >= 100) => 1,
					_ => 0
				};

				return (newPos.EuclideanRemainder(100), clicks + additionalClicks + fullRotations);

			}).clicks;
		}
	}
}