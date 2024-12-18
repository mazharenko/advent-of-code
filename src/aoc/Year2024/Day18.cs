using aoc.Common;
using aoc.Common.BfsImpl;
using mazharenko.AoCAgent.Generator;

namespace aoc.Year2024;

[BypassNoExamples]
internal partial class Day18
{
	private static Result<V<int>> RunSearch(HashSet<V<int>> blockingBytes)
	{
		return Bfs.Common.StartWith(V.Create(0, 0))
			.WithAdjacency(pos =>
			{
				return Directions.All4()
					.Select(dir => dir + pos)
					.Where(x => !blockingBytes.Contains(x))
					.Where(x => x is { X: >= 0 and <= 70, Y: >= 0 and <= 70 })
					.ToArray();
			}).WithTarget(Targets.Value(V.Create(70, 70)))
			.Run();
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

			var bs = RunSearch(blockingBytes);

			return bs switch
			{
				// todo: expose Len
				Found<V<int>> found => found.Path.PathList.Count() - 1
			};
		}
	}

	internal partial class Part2
	{
		public string Solve(V<int>[] input)
		{
			// todo: maybe collect all paths to target with Dijkstra and then remove the paths one by one containing the byte under consideration
			var blockingBytes = input.Take(1024).ToHashSet();
			var wonPositions = new HashSet<V<int>>();

			foreach (var anotherByte in input.Skip(1024))
			{
				blockingBytes.Add(anotherByte);
				if (wonPositions.Count != 0 && !wonPositions.Contains(anotherByte))
					continue;
				
				var bs = RunSearch(blockingBytes);
				switch (bs)
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