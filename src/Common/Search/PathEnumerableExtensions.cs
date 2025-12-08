namespace Common.Search;


public delegate bool Target<in T>(T state); // ITarget?

public static class Targets
{
	public static Target<T> Value<T>(T value)
		=> state => Equals(value, state);
}

public static class PathEnumerableExtensions
{
	public static Path<T> FindTarget<T>(this IEnumerable<Path<T>> paths, Target<T> target)
	{
		return paths.First(path => target(path.HeadItem));
	}
	
	public static IEnumerable<Path<T>> FindTarget<T>(this IEnumerable<(T node, List<Path<T>> paths)> paths, Target<T> target)
	{
		return paths.First(path => target(path.node)).paths;
	} // todo: TryFindTarget

	public static Result<T> TryFindTarget<T>(this IEnumerable<Path<T>> paths, Target<T> target)
	{
		foreach (var path in paths)
		{
			if (target(path.HeadItem))
				return new Found<T>(path);
		}
		
		return new NotFound<T>();
	}
	
	public static IEnumerable<T> Items<T>(this IEnumerable<Path<T>> paths)
		=> paths.Select(path => path.HeadItem);
}