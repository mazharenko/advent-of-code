namespace aoc.Common;

public static class EnumerableExtensions
{
	public static IEnumerable<(T, T)> AllPairs<T>(this IEnumerable<T> source)
	{
		// todo check f#
		return from x1 in source from x2 in source select (x1, x2);
	}
}