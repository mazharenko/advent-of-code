using System.Collections;

namespace aoc.Common.Search;

public static class Bfs
{
	public static BfsBuilder<T, T> StartWith<T>(T start)
	{
		return new BfsBuilder<T, T>(start, x => x);
	}
}

public class Bfs<T, TVisitedKey>(T start, IAdjacency<T> adjacency, Func<T, TVisitedKey>? visitedKey) : IEnumerable<Path<T>>
{
	public IEnumerator<Path<T>> GetEnumerator()
	{
		var visited = new HashSet<TVisitedKey?>();
		if (visitedKey is not null)
			visited.Add(visitedKey(start));
		var queue = new PriorityQueue<Path<T>, int>();
		queue.Enqueue(new Path<T>(L.Singleton(new PathItem<T>(start, 0))), int.MaxValue);
		while (true)
		{
			if (queue.Count == 0) yield break;
			var currentPath = queue.Dequeue();
			
			yield return currentPath;
			
			var current = currentPath.PathList.Head;
			var adjacent = adjacency.GetAdjacent(current.Item);
			foreach (var (adjacentValue, weight) in adjacent)
			{
				if (visitedKey is null || visited.Add(visitedKey(adjacentValue)))
					queue.Enqueue(new Path<T>(currentPath.PathList.Prepend(new PathItem<T>(adjacentValue, current.Len + weight))), current.Len + weight);
			}
		}
	}
	
	// todo: Enumerable<T>? Enumerable<PathItem<T>>?

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}