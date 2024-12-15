using aoc.Common;
using mazharenko.AoCAgent.Generator;
using MoreLinq;
using Spectre.Console;

namespace aoc.Year2024;

internal partial class Day15
{


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
	
		public (char[,] map, char[] moves) Parse(string input)
		{
			return Character.In('#', '.', 'O', '@').Map()
				.Block()
				.ThenBlock(
					Character.In('<', 'v', '>', '^')
						.Many().Lines()
						.Select(ar => ar.SelectMany(x => x).ToArray())
				).Parse(input);
		}

		public int Solve((char[,] map, char[] moves) input)
		{
			foreach (var move in input.moves)
			{
				var robotPos = input.map.AsEnumerable().Single(x => x.element == '@').point;
				var direction = move switch
				{
					'>' => Directions.Right,
					'<' => Directions.Left,
					'^' => Directions.Up,
					'v' => Directions.Down // todo: to parser
				};

				// todo: slice?

				// @
				// @ .
				// @ O O O O O O .
				// @ O O O O O O
				var untilWall =
					MoreEnumerable.Generate(robotPos, pos => pos + direction)
						.Where(input.map.Inside)
						.Select(pos => (pos, c: input.map.At(pos)))
						.TakeWhile(pos => pos.c is not '#')
						.TakeUntil(pos => pos.c is '.')
						.ToList();

				if (untilWall[^1].c == '.')
				{
					if (untilWall.Count == 2)
					{
						input.map[untilWall[0].pos.X, untilWall[0].pos.Y] = '.';
						input.map[untilWall[1].pos.X, untilWall[1].pos.Y] = '@';
					}
					else
					{
						input.map[untilWall[0].pos.X, untilWall[0].pos.Y] = '.';
						input.map[untilWall[1].pos.X, untilWall[1].pos.Y] = '@';
						input.map[untilWall[^1].pos.X, untilWall[^1].pos.Y] = 'O';
					}
				}


				// var stones = untilWall.Count(x => x.c == 'O');
				// var empty = untilWall.Count - stones;
				//
				// foreach (var (newChar, (pos, _)) in Enumerable.Repeat('.', empty)
				// 	         .Concat(Enumerable.Repeat('O', stones))
				// 	         .Zip(untilWall))
				// {
				// 	// todo: setter by V
				// 	input.map[(pos - direction).X, (pos - direction).Y] = newChar;
				// }
			}

			return input.map.AsEnumerable().Where(x => x.element == 'O')
				.Sum(x => x.point.X * 100 + x.point.Y);
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

		public (V<int> robot, V<int>[] stones, V<int>[] walls, char[] moves) Parse(string input)
		{
			var (map, moves) = Character.In('#', '.', 'O', '@').Map()
				.Block()
				.ThenBlock(
					Character.In('<', 'v', '>', '^')
						.Many().Lines()
						.Select(ar => ar.SelectMany(x => x).ToArray())
				).Parse(input);
			var all = map.AsEnumerable().ToArray();
			var walls =
				all.Choose(x => (x.element == '#', x.point))
					.SelectMany(x => new[] { V.Create(x.X, x.Y * 2), V.Create(x.X, x.Y * 2) + Directions.Right });
			var robot = all.Single(x => x.element == '@');
			var stones = all.Choose(x => (x.element == 'O', V.Create(x.point.X, x.point.Y * 2)));


			return (V.Create(robot.point.X, robot.point.Y * 2), stones.ToArray(), walls.ToArray(), moves);
		}

		private IEnumerable<V<int>> InvolvedStones(V<int> dir, V<int> from, HashSet<V<int>> stones, IDictionary<V<int>, V<int>> occupiedByStones)
		{
			if (dir == Directions.Up || dir == Directions.Down)
			{
				if (occupiedByStones.TryGetValue(from, out var stone))
				{
					return
						new[] { stone + dir, stone + dir + Directions.Right }
							.SelectMany(ss =>
								InvolvedStones(dir, ss, stones, occupiedByStones)
									.Append(stone))
						;
				}
			}
			else
			{
				if (occupiedByStones.TryGetValue(from, out var stone))
				{
					var ss = stone + dir * (dir == Directions.Right ? 2 : 1);
					if (Equals(from, ss))
						return [];
					return
						InvolvedStones(dir, ss, stones, occupiedByStones)
							.Append(stone);
				}
			}

			return [];
		}

		public int Solve((V<int> robot, V<int>[] stones, V<int>[] walls, char[] moves) input)
		{
			var robotPos = input.robot;
			var stones = input.stones.ToHashSet();
			var walls = input.walls.ToHashSet();
			var occupiedByStones = stones
				.SelectMany(s => new[] { (s, s), (s + Directions.Right, s) })
				.ToDictionary(x => x.Item1, x => x.Item2);
			AnsiConsole.Live(new Canvas(50, 50)).Start(ctx =>
			{
				foreach (var move in input.moves)
				{
					var direction = move switch
					{
						'>' => Directions.Right,
						'<' => Directions.Left,
						'^' => Directions.Up,
						'v' => Directions.Down // todo: to parser
					};
					var involvedStones = InvolvedStones(direction, robotPos + direction, stones, occupiedByStones).ToList();
					var allStonePoints = involvedStones.SelectMany(p => new[] { p, p + Directions.Right });
					var canMove = !allStonePoints.Select(x => x + direction).Any(walls.Contains)
					              && !walls.Contains(robotPos + direction);
					if (canMove)
					{
						//stones.IntersectWith(involvedStones);// todo: equals for V
						stones.RemoveWhere(stone => involvedStones.Any(s => s.X == stone.X && s.Y == stone.Y));
						stones.UnionWith(involvedStones.Select(s => s + direction));
						robotPos += direction;
						occupiedByStones = stones
							.SelectMany(s => new[] { (s, s), (s + Directions.Right, s) })
							.ToDictionary(x => x.Item1, x => x.Item2);
					}
/*
					Thread.Sleep(500);
					var canvas = new Canvas(50, 50);
					foreach (var stone in stones)
					{
						canvas.SetPixel(stone.Y, stone.X, Color.White);
						canvas.SetPixel(stone.Y + 1, stone.X, Color.Blue);
					}

					foreach (var wall in walls)
					{
						canvas.SetPixel(wall.Y, wall.X, Color.Grey);
					}

					canvas.SetPixel(robotPos.Y, robotPos.X, Color.Red);

					ctx.UpdateTarget(canvas);
*/				}
			});

			return stones.Sum(s => s.X * 100 + s.Y);
		}
	}
}