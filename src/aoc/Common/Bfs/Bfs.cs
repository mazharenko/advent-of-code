using aoc.Common.BfsBuilders;

// ReSharper disable once CheckNamespace
namespace aoc.Common;

public readonly record struct PathItem<T>(T Item, int Len);

public readonly record struct Path<T>(L<PathItem<T>> PathList)
{
	public T HeadItem { get; } = PathList.Head.Item;
}

public abstract record Result<T>; // todo: explicit match?
public record Found<T>(Path<T> Path) : Result<T>;
public record NotFound<T> : Result<T>;

public static class Bfs
{
	public static class Common
	{
		public interface IAdjacency<T>
		{
			IEnumerable<(T newState, int weight)> GetAdjacent(T pos);
		}

		public class AdhocAdjacency<T>(Func<T, IEnumerable<(T newState, int weight)>> function) : IAdjacency<T>
		{
			public IEnumerable<(T newState, int weight)> GetAdjacent(T pos) => function(pos);
		}


		public static BfsBuilder<T> StartWith<T>(T start)
		{
			return new BfsBuilder<T>(start);
		}
	}
}