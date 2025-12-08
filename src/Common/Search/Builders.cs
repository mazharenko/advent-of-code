namespace Common.Search;

public class DijkstraBuilder<T, TKey>(T start, Func<T, TKey> keyFunction) where T : notnull where TKey : notnull
{
	public DijkstraBuilder<T, TNewKey> WithKey<TNewKey>(Func<T, TNewKey> key) where TNewKey : notnull
	{
		return new DijkstraBuilder<T, TNewKey>(start, key);
	}
	
	public Dijkstra<T, TKey> WithAdjacency(Func<T, IEnumerable<(T newState, int weight)>> adjacency)
		=> WithAdjacency(new AdhocAdjacency<T>(adjacency));

	public Dijkstra<T, TKey> WithAdjacency(Func<T, IEnumerable<T>> adjacency)
		=> WithAdjacency(new AdhocAdjacency<T>(state => adjacency(state).Select(newState => (newState, 1))));

	public Dijkstra<T, TKey> WithAdjacency(IAdjacency<T> adjacency)
		=> new(start, adjacency, keyFunction);
}
