// ReSharper disable once CheckNamespace

namespace aoc.Common.BfsImpl;

public class Folder<T, TKey, TRes>(T start, Bfs.Common.IAdjacency<T> adjacency, Func<T, TKey?> visitedKey, TRes seed, IFold<T, TRes> folder)
{
	public TRes Run()
	{
		var visited = new HashSet<TKey?> { visitedKey(start) };
		var queue = new PriorityQueue<Path<T>, int>();
		queue.Enqueue(new Path<T>(L.Singleton(new PathItem<T>(start, 0))), int.MaxValue);
		return Fold(seed, visited, queue);
	}

	private TRes Fold(TRes state, HashSet<TKey?> visited, PriorityQueue<Path<T>,int> queue)
	{
		if (queue.Count == 0)
			return state;
		var currentPath = queue.Dequeue();
		var current = currentPath.PathList.Head;
		switch (folder.Fold(state, currentPath))
		{
			case (var newState, TraversalResult.Interrupt):
				return newState;
			case (var newState, TraversalResult.Continue):
				var adjacent = adjacency.GetAdjacent(current.Item);
				foreach (var (adjacentValue, weight) in adjacent)
				{
					var key = visitedKey(adjacentValue);
					if (key is null || visited.Add(key))
						queue.Enqueue(new Path<T>(currentPath.PathList.Prepend(new PathItem<T>(adjacentValue, current.Len + weight))), weight);
				}

				return Fold(newState, visited, queue);
			default: throw new InvalidOperationException();
		}
	}
}

public interface IFold<T, TRes>
{
	(TRes, TraversalResult) Fold(TRes acc, Path<T> path);
} 

public class AdhocFold<T, TRes>(Func<TRes, Path<T>, (TRes, TraversalResult)> function) : IFold<T, TRes>
{
	public (TRes, TraversalResult) Fold(TRes acc, Path<T> path)
	 => function(acc, path);
}

public class SearchFold<T>(Bfs.Common.Target<T> target) : IFold<T, Result<T>>
{
	public (Result<T>, TraversalResult) Fold(Result<T> acc, Path<T> path)
	{
		var (current, _) = path.PathList;
		if (target(current.Item))
			return (new Found<T>(path), TraversalResult.Interrupt);
		return (new NotFound<T>(), TraversalResult.Continue);
	}
}

public enum TraversalResult
{
	Continue,
	Interrupt
}