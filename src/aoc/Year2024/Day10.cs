using aoc.Common;
using MoreLinq;

namespace aoc.Year2024;

internal partial class Day10 // todo: common bfs?
{
	internal partial class Part1
	{
		private readonly Example example = new Example(
			"""
			89010123
			78121874
			87430965
			96549874
			45678903
			32019012
			01329801
			10456732
			""", 36);

		public int Solve(int[,] input)
		{
			var trailHeads = input.AsEnumerable().Where(x => x.element == 0).ToArray();

			return trailHeads.Sum(head =>
			{
				var list = new HashSet<V<int>>();
				FillVisited(input, head.point, list);
				return list.Count;
			});
		}

		private static void FillVisited(int[,] map, V<int> curPos, HashSet<V<int>> list)
		{
			if (map.At(curPos) == 9)
				list.Add(curPos);
			var adj = new[]
				{
					Directions.Down(),
					Directions.Up(),
					Directions.Right(),
					Directions.Left(),
				}.Select(d => d + curPos)
				.Where(map.Inside);

			adj
				.Where(p => map.At(p) == map.At(curPos) + 1)
				.ForEach(p => FillVisited(map, p, list));
		}

		public int[,] Parse(string input)
		{
			return Character.Digit.Select(c => int.Parse(c.ToString()))
				.Map().Parse(input);
		}
	}

	internal partial class Part2
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
			""", 81);

		public int Solve(int[,] input)
		{
			var trailHeads = input.AsEnumerable().Where(x => x.element == 0).ToArray();

			return trailHeads.Sum(head => Score(input, head.point));
		}

		private static int Score(int[,] map, V<int> curPos)
		{
			if (map.At(curPos) == 9)
				return 1;
			var adj = new[]
				{
					Directions.Down(),
					Directions.Up(),
					Directions.Right(),
					Directions.Left(),
				}.Select(d => d + curPos)
				.Where(map.Inside);

			return adj
				.Where(p => map.At(p) == map.At(curPos) + 1)
				.Sum(p => Score(map, p));
		}

		public int[,] Parse(string input)
		{
			return Character.Digit.Select(c => int.Parse(c.ToString()))
				.Map().Parse(input);
		}
	}
}