using aoc.Common;
using mazharenko.AoCAgent.Generator;
using MoreLinq;

namespace aoc.Year2024;

internal abstract partial class Day15
{
	public virtual (V<int> robot, V<int>[] stones, V<int>[] walls, V<int>[] moves) Parse(string input)
	{
		return Character.In('#', '.', 'O', '@').Map()
			.Block()
			.ThenBlock(
				Character.In('<', 'v', '>', '^')
					.Many().Lines()
					.Select(ar => ar.SelectMany(x => x)
						.Select(c => c switch
						{
							'>' => Directions.Right,
							'<' => Directions.Left,
							'^' => Directions.Up,
							'v' => Directions.Down
						}).ToArray())
			)
			.Select(t =>
			{
				var (map, moves) = t;
				var allPoints = map.AsEnumerable().ToArray();
				var robot = allPoints.Single(x => x.element == '@').point;
				var walls = allPoints.Where(x => x.element == '#').Select(x => x.point).ToArray();
				var stones = allPoints.Where(x => x.element == 'O').Select(x => x.point).ToArray();
				return (robot, stones, walls, moves);
			}).Parse(input);
	}

	protected abstract IEnumerable<V<int>> StonePoints(V<int> stonePos);

	private int Solve((V<int> robot, V<int>[] stones, V<int>[] walls, V<int>[] moves) input)
	{
		var robotPos = input.robot;
		var walls = input.walls.ToHashSet();
		var occupiedByStones = OccupiedByStones(input.stones.ToHashSet());
		foreach (var move in input.moves)
		{
			var direction = move;
			var involvedStones = InvolvedStones(direction, robotPos + direction, occupiedByStones).ToList();
			var allStonePoints = involvedStones.SelectMany(StonePoints).ToList();
			var canMove = !allStonePoints.Select(x => x + direction).Any(walls.Contains)
			              && !walls.Contains(robotPos + direction);
			if (canMove)
			{
				foreach (var stonePoint in allStonePoints)
				{
					occupiedByStones.Remove(stonePoint);
				}
				foreach (var stone in involvedStones)
				{
					foreach (var stonePoint in StonePoints(stone))
					{
						occupiedByStones[stonePoint + direction] = stone + direction;
					}
				}

				robotPos += direction;
			}
		}

		return occupiedByStones.Values.Distinct().Sum(s => s.X * 100 + s.Y);
	}

	private Dictionary<V<int>, V<int>> OccupiedByStones(HashSet<V<int>> stones)
	{
		return stones
			.SelectMany(s => StonePoints(s).Select(p => (p, s)))
			.ToDictionary(x => x.Item1, x => x.Item2);
	}

	protected abstract IEnumerable<V<int>> InvolvedStones(V<int> dir, V<int> from,
		IDictionary<V<int>, V<int>> occupiedByStones);


	internal partial class Part1
	{
		private readonly Example example = new(
			"""
			########
			#..O.O.#
			##@.O..#
			#...O..#
			#.#.O..#
			#...O..#
			#......#
			########

			<^^>>>vv<v>>v<<
			""", 2028);

		protected override IEnumerable<V<int>> StonePoints(V<int> stonePos)
		{
			return [stonePos];
		}

		// source generator needs this. maybe want some improvement
		public new int Solve((V<int> robot, V<int>[] stones, V<int>[] walls, V<int>[] moves) input)
		{
			return base.Solve(input);
		}

		protected override IEnumerable<V<int>> InvolvedStones(V<int> dir, V<int> from,
			IDictionary<V<int>, V<int>> occupiedByStones)
		{
			return MoreEnumerable.GenerateByIndex(i => dir * i + from)
				.TakeWhile(occupiedByStones.ContainsKey);
		}
	}

	[BypassNoExamples]
	internal partial class Part2
	{
		private readonly Example example = new(
			"""
			##########
			#..O..O.O#
			#......O.#
			#.OO..O.O#
			#..O@..O.#
			#O#..O...#
			#O..O..O.#
			#.OO.O.OO#
			#....O...#
			##########

			<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
			vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
			><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
			<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
			^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
			^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
			>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
			<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
			^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
			v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
			""", 9021);

		protected override IEnumerable<V<int>> StonePoints(V<int> stonePos)
		{
			return [stonePos, stonePos + Directions.Right];
		}

		public new int Solve((V<int> robot, V<int>[] stones, V<int>[] walls, V<int>[] moves) input)
		{
			V<int> Double(V<int> p) => V.Create(p.X, p.Y * 2);
			var doubleInput =
			(
				robot: Double(input.robot),
				stones: input.stones.Select(Double).ToArray(),
				walls: input.walls.SelectMany(wall => new[] { Double(wall), Double(wall) + Directions.Right }).ToArray(),
				input.moves
			);
			return base.Solve(doubleInput);
		}

		protected override IEnumerable<V<int>> InvolvedStones(V<int> dir, V<int> from,
			IDictionary<V<int>, V<int>> occupiedByStones)
		{
			if (dir == Directions.Up || dir == Directions.Down)
			{
				if (occupiedByStones.TryGetValue(from, out var stone))
				{
					return
						new[] { stone + dir, stone + dir + Directions.Right }
							.SelectMany(ss =>
								InvolvedStones(dir, ss, occupiedByStones)
									.Append(stone)).Distinct();
				}
			}
			else
			{
				return MoreEnumerable.GenerateByIndex(i => dir * i + from)
						.TakeWhile(occupiedByStones.ContainsKey)
						.Select(x => occupiedByStones[x])
						.Distinct() ;
			}

			return [];
		}
	}
}