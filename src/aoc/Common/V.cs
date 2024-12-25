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

	public static T MDist<T>(this V<T> v1, V<T> v2) where T : INumber<T>
	{
		return T.Abs(v1.X - v2.X) + T.Abs(v1.Y - v2.Y);
	}
}

public static class Directions
{
	public static V<int> Down {get;} = new(1, 0);
	public static V<int> S {get;} = Down;

	public static V<int> Up {get;} = new(-1, 0);
	public static V<int> N {get;} = Up;

	public static V<int> Right {get;} = new(0, 1);
	public static V<int> E {get;} = Right;
	
	public static V<int> Left {get;} = new(0, -1);
	public static V<int> W {get;} = Left;

	// ReSharper disable InconsistentNaming
	public static V<int> NE {get;} = N + E;
	public static V<int> SE {get;} = S + E;
	public static V<int> NW {get;} = N + W;
	public static V<int> SW {get;} = S + W;
	// ReSharper restore InconsistentNaming
	
	public static V<int>[] All4() =>
	[
		Down, Left, Up, Right
	];

	public static V<int>[] All8() =>
	[
		N, NE, E, SE, S, SW, W, NW
	];
}

public record V<T>(T X, T Y) where T : INumber<T>
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

	public static V<T> operator -(V<T> v)
	{
		return v * -1;
	}

	public static V<T> Zero { get; } = new(T.Zero, T.Zero);
	public static V<T> One { get; } = new(T.One, T.One);
	
}