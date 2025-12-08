namespace aoc.Common;

public static class EnumerableExtensions
{
	extension(Range range)
	{
		public IEnumerable<int> AsEnumerable()
		{
			var (offset, length) = range.GetOffsetAndLength(int.MaxValue);
			return Enumerable.Range(offset, length);
		}
	}

	public static IEnumerable<TResult> UniquePairs<T, TResult>(this IList<T> source, Func<T, T, TResult> resultSelector)
	{
		for (var i = 0; i < source.Count; i++)
		for (var j = i + 1; j < source.Count; j++)
			yield return resultSelector(source[i], source[j]);
	}
	
	public static IEnumerable<(T first, T second)> UniquePairs<T>(this IList<T> source)
		=> UniquePairs(source, (a, b) => (a, b));
}