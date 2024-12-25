using aoc.Common;
using aoc.Common.Search;
using mazharenko.AoCAgent.Generator;

namespace aoc.Year2024;

[BypassNoExamples]
internal partial class Day20
{
	public char[,] Parse(string input)
	{
		return Character.In('.', '#', 'S', 'E').Map().Parse(input);
	}

	private static int Solve(char[,] map, int shortcutLength)
	{
		var shortcuts =
			map.AsEnumerable().Where(x => x.element != '#')
				.SelectMany(x =>
					map.AsEnumerable().Where(y => y.element != '#')
						.Where(y => x.point.MDist(y.point) <= shortcutLength)
						.Select(y => (start: x.point, end: y.point))
				)
				.ToList();

		var start = map.AsEnumerable().Single(x => x.element == 'S').point;
		var end = map.AsEnumerable().Single(x => x.element == 'E').point;

		var forward = Bfs.StartWith(start)
			.WithAdjacency(x =>
				Directions.All4().Select(d => d + x)
					.Where(y => map.At(y) != '#'))
			.ToDictionary(path => path.HeadItem, path => path.Len);

		var backward = Bfs.StartWith(end)
			.WithAdjacency(x =>
				Directions.All4().Select(d => d + x)//todo: extension
					.Where(y => map.At(y) != '#'))
			.ToDictionary(path => path.HeadItem, path => path.Len);

		var noShortcutsPath = forward[end];

		return shortcuts.Count(shortcut =>
			noShortcutsPath
			- (shortcut.start.MDist(shortcut.end) + forward[shortcut.start] + backward[shortcut.end]) >= 100);
	}

	internal partial class Part1
	{
		public int Solve(char[,] input) => Solve(input, 2);
	}

	internal partial class Part2
	{
		public int Solve(char[,] input) => Solve(input, 20);
	}
}