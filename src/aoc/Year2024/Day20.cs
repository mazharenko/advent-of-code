using aoc.Common;
using aoc.Common.BfsImpl;
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
						.Where(y => x.point.MLen(y.point) <= shortcutLength)
						.Select(y => (start: x.point, end: y.point))
				)
				.ToList();

		var start = map.AsEnumerable().Single(x => x.element == 'S').point;
		var end = map.AsEnumerable().Single(x => x.element == 'E').point;
		
		var forward = Bfs.Common.StartWith(start)
			.WithAdjacency(x =>
				Directions.All4().Select(d => d + x)
					.Where(y => map.At(y) != '#'))
			.WithFolder(new Dictionary<V<int>, int>(), (acc, path) =>
			{
				// todo: I need bfs as a sequence generator!!!
				acc[path.HeadItem] = path.PathList.Head.Len;
				return (acc, TraversalResult.Continue);
			}).Run();

		var backward = Bfs.Common.StartWith(end)
			.WithAdjacency(x =>
				Directions.All4().Select(d => d + x)//todo: extension
					.Where(y => map.At(y) != '#'))
			.WithFolder(new Dictionary<V<int>, int>(), (acc, path) =>
			{
				// todo: I need bfs as an enumerator!!!
				acc[path.HeadItem] = path.PathList.Head.Len;
				return (acc, TraversalResult.Continue);
			}).Run();

		var noShortcutsPath = forward[end];

		return shortcuts.Count(shortcut =>
			noShortcutsPath
			- (shortcut.start.MLen(shortcut.end) + forward[shortcut.start] + backward[shortcut.end]) >= 100);
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