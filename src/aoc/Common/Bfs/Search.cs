// ReSharper disable once CheckNamespace

namespace aoc.Common.BfsImpl;

public delegate bool Target<in T>(T state); // ITarget?

public static class Targets
{
	public static Target<T> Value<T>(T value)
		=> state => Equals(value, state);
}

public class SearchFold<T>(Target<T> target) : IFold<T, Result<T>>
{
	public (Result<T>, TraversalResult) Fold(Result<T> acc, Path<T> path)
	{
		var (current, _) = path.PathList;
		if (target(current.Item))
			return (new Found<T>(path), TraversalResult.Interrupt);
		return (new NotFound<T>(), TraversalResult.Continue);
	}
}