namespace aoc.Common.Search;

public readonly record struct PathItem<T>(T Item, int Len);

public readonly record struct Path<T>(L<PathItem<T>> PathList)
{
	public T HeadItem { get; } = PathList.Head.Item;
	public int Len => PathList.Head.Len;
}

public abstract record Result<T>;

public static class ResultExtensions
{
	public static TRes Match<T, TRes>(this Result<T> result, Func<Path<T>, TRes> onFound, Func<TRes> onNotFound)
	{
		return result switch
		{
			Found<T> found => onFound(found.Path),
			NotFound<T> => onNotFound(),
			_ => throw new ArgumentOutOfRangeException(nameof(result))
		};
	}

	public static void Match<T>(this Result<T> result, Action<Path<T>> onFound, Action onNotFound)
	{
		switch (result)
		{
			case Found<T> found:
				onFound(found.Path);
				break;
			case NotFound<T>:
				onNotFound();
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(result));
		}
	}

	public static Path<T> AsFound<T>(this Result<T> result) =>
		Match(result, p => p, () => throw new InvalidOperationException("The result was not 'Found'"));
}

public record Found<T>(Path<T> Path) : Result<T>;

public record NotFound<T> : Result<T>;

public interface IAdjacency<T>
{
	IEnumerable<(T newState, int weight)> GetAdjacent(T state);
}

public class AdhocAdjacency<T>(Func<T, IEnumerable<(T newState, int weight)>> function) : IAdjacency<T>
{
	public IEnumerable<(T newState, int weight)> GetAdjacent(T pos) => function(pos);
}