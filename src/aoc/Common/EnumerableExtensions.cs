namespace aoc.Common;

public static class EnumerableExtensions
{
	public static IEnumerable<int> AsEnumerable(this Range range)
	{
		var (offset, length) = range.GetOffsetAndLength(int.MaxValue);
		return Enumerable.Range(offset, length);
	}
}