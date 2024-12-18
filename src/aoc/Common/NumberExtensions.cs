using System.Numerics;

namespace aoc.Common;

public static class NumberExtensions
{
	public static T EuclideanRemainder<T>(this T a, T b) where T : INumber<T>
	{
		if (b <= T.Zero) throw new ArgumentOutOfRangeException(nameof(b));
		return (a % b + b) % b;
	}
}