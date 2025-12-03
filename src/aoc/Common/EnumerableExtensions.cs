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
}