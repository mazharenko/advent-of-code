namespace aoc.Common.Search;

public class BfsBuilder<T, TVisitedKey>(T start, Func<T, TVisitedKey>? visitedKeyFunction)
{
	public BfsBuilder<T, TNewKey> WithVisitedKey<TNewKey>(Func<T, TNewKey> visited)
	{
		return new BfsBuilder<T, TNewKey>(start, visited);
	}

	public BfsBuilder<T, object> WithoutTrackingVisited()
	{
		return new BfsBuilder<T, object>(start, null);
	}

	public Bfs<T, TVisitedKey> WithAdjacency(Func<T, (T newState, int weight)[]> adjacency)
		=> WithAdjacency(new AdhocAdjacency<T>(adjacency));

	public Bfs<T, TVisitedKey> WithAdjacency(Func<T, IEnumerable<T>> adjacency)
		=> WithAdjacency(new AdhocAdjacency<T>(state => adjacency(state).Select(newState => (newState, 1))));

	public Bfs<T, TVisitedKey> WithAdjacency(IAdjacency<T> adjacency)
		=> new(start, adjacency, visitedKeyFunction);
}