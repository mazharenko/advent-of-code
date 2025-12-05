using System.Numerics;

namespace aoc.Common;

public static class R
{
	public static R<T>[] Union<T>(R<T> range1, R<T> range2) where T : INumber<T>
	{
		if (range1.Start > range2.Start)
			return Union(range2, range1);
		if (range2.Start <= range1.End + T.One)
			return [range1 with { End = T.Max(range1.End, range2.End) }];
		return [range1, range2];
	}

	public static R<T>[] UnionAll<T>(R<T>[] ranges) where T : INumber<T>
	{
		var sorted = ranges.OrderBy(range => range.Start);
		return sorted.Aggregate(L<R<T>>.Empty,
			(acc, r) =>
			{
				if (acc.IsEmpty)
					return L.Singleton(r);
				var (last, rest) = acc;
				return L.Create(Union(last, r).Reverse().ToList()).Append(rest);
			}
		).ToArray();
	}
}

public record R<T>(T Start, T End) where T : INumber<T>
{
	public T Len => End - Start + T.One;

	public bool Contains(T value) => value >= Start && value <= End;
}