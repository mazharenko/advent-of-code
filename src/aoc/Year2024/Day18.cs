using aoc.Common;
using aoc.Common.Search;
using mazharenko.AoCAgent.Generator;

namespace aoc.Year2024;

[BypassNoExamples]
internal partial class Day18
{
	private static Result<V<int>> RunSearch(HashSet<V<int>> blockingBytes)
	{
		return Dijkstra.StartWith(V.Create(0, 0))
			.WithAdjacency(pos =>
			{
				return Directions.All4()
					.Select(dir => dir + pos)
					.Where(x => !blockingBytes.Contains(x))
					.Where(x => x is { X: >= 0 and <= 70, Y: >= 0 and <= 70 })
					.ToArray();
			}).TryFindTarget(Targets.Value(V.Create(70, 70)));
	}

	public V<int>[] Parse(string input)
	{
		return Numerics.IntegerInt32.ThenIgnore(Span.EqualTo(","))
			.Then(Numerics.IntegerInt32)
			.Select(V.Create)
			.Lines().Parse(input);
	}

	internal partial class Part1
	{
		public int Solve(V<int>[] input)
		{
			var blockingBytes = input.Take(1024).ToHashSet();
			return RunSearch(blockingBytes).AsFound().Len;
		}
	}

	internal partial class Part2
	{
		public string Solve(V<int>[] input)
		{
			var blockingBytes = input.Take(1024).ToHashSet();
			var wonPositions = new HashSet<V<int>>();

			foreach (var anotherByte in input.Skip(1024))
			{
				blockingBytes.Add(anotherByte);
				if (wonPositions.Count != 0 && !wonPositions.Contains(anotherByte))
					continue;

				switch (RunSearch(blockingBytes))
				{
					case Found<V<int>> (var path):
						wonPositions.UnionWith(path.PathList.Select(pi => pi.Item));
						break;
					case NotFound<V<int>>:
						return $"{anotherByte.X},{anotherByte.Y}";
				}
			}

			throw new Exception();
		}
	}
}