namespace Common;

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

	extension<T>(IList<T> source)
	{
		public IEnumerable<TResult> UniquePairs<TResult>(Func<T, T, TResult> resultSelector)
		{
			for (var i = 0; i < source.Count; i++)
			for (var j = i + 1; j < source.Count; j++)
				yield return resultSelector(source[i], source[j]);
		}

		public IEnumerable<(T first, T second)> UniquePairs()
			=> UniquePairs(source, (a, b) => (a, b));
	}

	extension<T>(IEnumerable<T> source)
	{
		public IEnumerable<TRes> Choose<TRes>(Func<T, Maybe<TRes>> chooser)
		{
			return source.Select(chooser)
				.Where(m => m.HasValue)
				.Select(m => m.Value);
		}
	}
}