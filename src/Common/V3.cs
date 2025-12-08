using System.Numerics;

namespace Common;

public static class V3
{
	public static V3<T> Create<T>(T x, T y, T z) where T : INumber<T>
	{
		return new V3<T>(x, y, z);
	}

	public static V3<T> Dir<T>(this V3<T> p) where T : INumber<T>
	{
		return Create(T.CreateChecked(T.Sign(p.X)), T.CreateChecked(T.Sign(p.Y)), T.CreateChecked(T.Sign(p.Z)));
	}

	public static T MDist<T>(this V3<T> v1, V3<T> v2) where T : INumber<T>
	{
		return T.Abs(v1.X - v2.X) + T.Abs(v1.Y - v2.Y) + T.Abs(v1.Z - v2.Z);
	}

	public static double Dist<T>(this V3<T> v1, V3<T> v2) where T : INumber<T>
	{
		var square =
			(v1.X - v2.X) * (v1.X - v2.X)
			+ (v1.Y - v2.Y) * (v1.Y - v2.Y)
			+ (v1.Z - v2.Z) * (v1.Z - v2.Z);
		return Math.Sqrt(double.CreateSaturating(square));
	}
}

public record V3<T>(T X, T Y, T Z) where T : INumber<T>
{
	public V3((T X, T Y, T Z) tuple) : this(tuple.X, tuple.Y, tuple.Z)
	{
	}

	public static V3<T> operator +(V3<T> v1, V3<T> v2)
	{
		return new V3<T>(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
	}

	public static V3<T> operator -(V3<T> v1, V3<T> v2)
	{
		return new V3<T>(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
	}

	public static V3<T> operator +(V3<T> v1, V3<int> v2)
	{
		return new V3<T>(v1.X + T.CreateChecked(v2.X), v1.Y + T.CreateChecked(v2.Y), v1.Z + T.CreateChecked(v2.Z));
	}

	public static V3<T> operator -(V3<T> v1, V3<int> v2)
	{
		return new V3<T>(v1.X - T.CreateChecked(v2.X), v1.Y - T.CreateChecked(v2.Y), v1.Z - T.CreateChecked(v2.Z));
	}
	
	public static V3<T> operator *(V3<T> v1, T n)
	{
		return new V3<T>(v1.X * n, v1.Y * n, v1.Z * n);
	}
	
	public static V3<T> operator *(V3<T> v1, int n)
	{
		return new V3<T>(v1.X * T.CreateChecked(n), v1.Y * T.CreateChecked(n), v1.Z * T.CreateChecked(n));
	}

	public static V3<T> operator -(V3<T> v)
	{
		return v * -1;
	}

	public static V3<T> Zero { get; } = new(T.Zero, T.Zero, T.Zero);
	public static V3<T> One { get; } = new(T.One, T.One, T.One);
}