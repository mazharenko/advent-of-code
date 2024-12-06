using System.Numerics;

namespace aoc.Common;

public static class V
{
	public static V<T> Create<T>(T x, T y) where T : INumber<T>
	{
		return new V<T>(x, y);
	}
	public static V<T> Dir<T>(this V<T> p) where T : INumber<T>
	{
		return Create(T.CreateChecked(T.Sign(p.X)), T.CreateChecked(T.Sign(p.Y)));
	}

	public static V<T> RotateCw<T>(this V<T> v) where T : INumber<T> 
		=> Create(v.Y, -v.X);

	public static V<T> RotateCcw<T>(this V<T> v) where T : INumber<T>
		=> Create(-v.Y, v.X);
}

public static class Directions
{
	public static V<int> Down() => new(1, 0);

	public static V<int> Up() => new(-1, 0);

	public static V<int> Right() => new(0, 1);

	public static V<int> Left() => new(0, -1);
}

public record struct V<T>(T X, T Y) where T : INumber<T>
{
	public V((T X, T Y) tuple) : this(tuple.X, tuple.Y)
	{
	}

	public static V<T> operator +(V<T> v1, V<T> v2)
	{
		return new V<T>(v1.X + v2.X, v1.Y + v2.Y);
	}

	public static V<T> operator -(V<T> v1, V<T> v2)
	{
		return new V<T>(v1.X - v2.X, v1.Y - v2.Y);
	}

	public static V<T> operator +(V<T> v1, V<int> v2)
	{
		return new V<T>(v1.X + T.CreateChecked(v2.X), v1.Y + T.CreateChecked(v2.Y));
	}

	public static V<T> operator -(V<T> v1, V<int> v2)
	{
		return new V<T>(v1.X - T.CreateChecked(v2.X), v1.Y - T.CreateChecked(v2.Y));
	}
	
	public static V<T> operator *(V<T> v1, T n)
	{
		return new V<T>(v1.X * n, v1.Y * n);
	}
	
	public static V<T> operator *(V<T> v1, int n)
	{
		return new V<T>(v1.X * T.CreateChecked(n), v1.Y * T.CreateChecked(n));
	}
	
	public static V<T> Zero { get; } = new(T.Zero, T.Zero);
	public static V<T> One { get; } = new(T.One, T.One);
	
}