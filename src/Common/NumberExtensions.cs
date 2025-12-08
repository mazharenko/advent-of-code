using System.Numerics;

namespace Common;

public static class NumberExtensions
{
	extension<T>(T a) where T : INumber<T>
	{
		public T EuclideanRemainder(T b)
		{
			if (b <= T.Zero) throw new ArgumentOutOfRangeException(nameof(b));
			return (a % b + b) % b;
		}
	}
}