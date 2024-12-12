using aoc.Common.BfsImpl;
using static aoc.Common.Bfs.Common;

// ReSharper disable once CheckNamespace
namespace aoc.Common.BfsBuilders;

public class BfsBuilder<T>(T start)
{
	public BfsAdjBuilder<T, T> WithAdjacency(Func<T, (T newState, int weight)[]> adjacency)
		=> WithAdjacency(new AdhocAdjacency<T>(adjacency));

	public BfsAdjBuilder<T, T> WithAdjacency(Func<T, IEnumerable<T>> adjacency)
		=> WithAdjacency(new AdhocAdjacency<T>(state => adjacency(state).Select(newState => (newState, 1))));

	public BfsAdjBuilder<T, T> WithAdjacency(IAdjacency<T> adjacency)
		=> new(start, adjacency, x => x);
}

public class BfsAdjBuilder<T, TKey>(T start, IAdjacency<T> adjacency, Func<T, TKey?> visitedKey)
{
	public BfsAdjBuilder<T, TNewKey> WithVisitedKey<TNewKey>(Func<T, TNewKey?> visitedKeyFunction)
	{
		return new BfsAdjBuilder<T, TNewKey>(start, adjacency, visitedKeyFunction);
	}

	public BfsAdjBuilder<T, object> WithoutTrackingVisited()
	{
		return new BfsAdjBuilder<T, object>(start, adjacency, x => null);
	}

	public Folder<T, TKey, TRes> WithFolder<TRes>(TRes seed, Func<TRes, Path<T>, (TRes, TraversalResult)> function)
	{
		return WithFolder(seed, new AdhocFold<T, TRes>(function));
	}

	public Folder<T, TKey, TRes> WithFolder<TRes>(TRes seed, IFold<T, TRes> fold)
	{
		return new Folder<T, TKey, TRes>(start, adjacency, visitedKey, seed, fold);
	}

	public Folder<T, TKey, Result<T>> WithTarget(Target<T> target)
	{
		return new Folder<T, TKey, Result<T>>(start, adjacency, visitedKey, new NotFound<T>(), new SearchFold<T>(target));
	}
}