using System.Collections;

namespace aoc.Common.Search;

public static class Dijkstra
{
	public static DijkstraBuilder<T, T> StartWith<T>(T start) where T : notnull
	{
		return new DijkstraBuilder<T, T>(start, x => x);
	}
}

public class Dijkstra<T, TKey>(T start, IAdjacency<T> adjacency, Func<T, TKey> keyFunction)
	: IEnumerable<Path<T>> where TKey : notnull where T : notnull
{
	public IEnumerable<Path<T>> WithoutRelaxation()
	{
		var queue = new PriorityQueue<Path<T>, int>();
		queue.Enqueue(new Path<T>(L.Singleton(new PathItem<T>(start, 0))), 0);
		while (true)
		{
			if (queue.Count == 0)
				yield break;

			var currentPath = queue.Dequeue();

			yield return currentPath;

			var adjacent = adjacency.GetAdjacent(currentPath.HeadItem);
			foreach (var (next, nextDistance) in adjacent)
			{
				queue.Enqueue(new Path<T>(currentPath.PathList.Prepend(new PathItem<T>(next, currentPath.Len + nextDistance))),
					currentPath.Len + nextDistance);
			}
		}
	}

	public IEnumerable<(T node, List<Path<T>> paths)> AllShortestPaths()
	{
		var foundPaths = new Dictionary<TKey, List<Path<T>>>();
		var queue = new PriorityQueue<(T, int), int>();
		foundPaths[keyFunction(start)] = [new Path<T>(L.Singleton(new PathItem<T>(start, 0)))];
		queue.Enqueue((start, 0), 0);
		while (true)
		{
			if (queue.Count == 0)
				yield break;

			var (current, currentDist) = queue.Dequeue();

			var currentPaths = foundPaths[keyFunction(current)];
			if (currentDist > currentPaths[0].Len)
				continue;

			yield return (currentPaths[0].HeadItem, currentPaths);

			var adjacent = adjacency.GetAdjacent(current);
			foreach (var (next, nextDistance) in adjacent)
			{
				if (foundPaths.TryGetValue(keyFunction(next), out var previouslyFoundNextPaths))
				{
					var foundLen = previouslyFoundNextPaths[0].Len;
					if (foundLen > nextDistance + currentDist)
					{
						foundPaths[keyFunction(next)] = foundPaths[keyFunction(current)].Select(currentPath =>
							new Path<T>(currentPath.PathList.Prepend(new PathItem<T>(next, nextDistance + currentDist)))).ToList();
						queue.Enqueue((next, nextDistance + currentDist), nextDistance + currentDist);
					}
					else if (foundLen == nextDistance + currentDist)
					{
						foundPaths[keyFunction(next)].AddRange(foundPaths[keyFunction(current)].Select(currentPath =>
							new Path<T>(currentPath.PathList.Prepend(new PathItem<T>(next, nextDistance + currentDist)))));
					}
				}
				else
				{
					foundPaths[keyFunction(next)] = foundPaths[keyFunction(current)].Select(currentPath =>
						new Path<T>(currentPath.PathList.Prepend(new PathItem<T>(next, nextDistance + currentDist)))).ToList();
					queue.Enqueue((next, nextDistance + currentDist), nextDistance + currentDist);
				}
			}
		}
	}

	public IEnumerator<Path<T>> GetEnumerator()
	{
		var foundPaths = new Dictionary<TKey, Path<T>>();
		var queue = new PriorityQueue<(T, int), int>();
		foundPaths[keyFunction(start)] = new Path<T>(L.Singleton(new PathItem<T>(start, 0)));
		queue.Enqueue((start, 0), 0);
		while (true)
		{
			if (queue.Count == 0)
				yield break;

			var (current, currentDist) = queue.Dequeue();

			var currentPath = foundPaths[keyFunction(current)];
			if (currentDist > currentPath.Len)
				continue;

			yield return foundPaths[keyFunction(current)];

			var adjacent = adjacency.GetAdjacent(current);
			foreach (var (next, nextDistance) in adjacent)
			{
				if (!foundPaths.TryGetValue(keyFunction(next), out var previouslyFoundNextPath)
				    || previouslyFoundNextPath.Len > nextDistance + currentDist)
				{
					foundPaths[keyFunction(next)] =
						new Path<T>(currentPath.PathList.Prepend(new PathItem<T>(next, nextDistance + currentDist)));
					queue.Enqueue((next, nextDistance + currentDist), nextDistance + currentDist);
				}
			}
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}