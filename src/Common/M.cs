using System.Collections;
using System.Diagnostics.CodeAnalysis;
using MoreLinq;

namespace Common;

public class M<T>(T[,] map) : IEnumerable<(V<int> point, T element)>
{
	public int Height { get; } = map.GetLength(0);
	public int Width { get; } = map.GetLength(1);

	public IEnumerator<(V<int>, T)> GetEnumerator()
	{
		for (var i = 0; i < Height; i++)
		for (var j = 0; j < Width; j++)
			yield return (V.Create(i, j), map[i, j]);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public T this[V<int> p]
	{
		get => map[p.X, p.Y];
		set => map[p.X, p.Y] = value;
	}

	public T this[int x, int y]
	{
		get => map[x, y];
		set => map[x, y] = value;
	}
	
	public Maybe<T> TryAt(V<int> point)
	{
		if (TryAt(point, out var result))
			return result;
		return Maybe.None;
	}

	public bool TryAt(V<int> point, [MaybeNullWhen(false)] out T element)
	{
		if (point.X < 0 || point.X >= Height
		                || point.Y < 0 || point.Y >= Width)
		{
			element = default;
			return false;
		}

		element = this[point.X, point.Y];
		return true;
	}
}

public static class M
{
	public static M<T> Create<T>(T[,] map) => new(map);

	public static M<T> Init<T>(int height, int width, Func<int, int, T> initializer)
	{
		var map = new T[height, width];

		for (var i = 0; i < height; i++)
		for (var j = 0; j < width; j++)
			map[i, j] = initializer(i, j);

		return Create(map);
	}

	public static M<T> Init<T>(int height, int width, Func<V<int>, T> initializer) 
		=> Init(height, width, (i, j) => initializer(V.Create(i, j)));

	public static M<T> FromJagged<T>(T[][] jagged)
	{
		if (jagged.Length == 0)
			return Create(new T[0, 0]);
		var map = new T[jagged.Length, jagged[0].Length];
		for (var i = 0; i < map.GetLength(0); i++)
		for (var j = 0; j < map.GetLength(1); j++)
			map[i, j] = jagged[i][j];
		return Create(map);
	}

	extension<T>(M<T> source)
	{
		public M<U> Map<U>(Func<V<int>, T, U> mapper)
		{
			return Init(source.Height, source.Width, p => mapper(p, source[p]));
		}

		public void Iter(Action<V<int>, T> action)
		{
			source.ForEach(x => action(x.point, x.element));
		}
		
		public bool TrySet(V<int> point, T value)
		{
			if (source.Inside(point))
			{
				source[point.X, point.Y] = value;
				return true;
			}
			return false;
		}
		
		public bool Inside(V<int> point) => source.TryAt(point, out _);


		public bool Compare(M<T> map2)
		{
			if (source.Height != map2.Height
			    || source.Width != map2.Width)
				return false;
			for (var i = 0; i < source.Height; i++)
			for (var j = 0; j < source.Width; j++)
			{
				if (!Equals(source[i, j], map2[i, j]))
					return false;
			}

			return true;
		}

		public bool CompareFromSafe(int iFrom, int jFrom, M<T> map2, Func<T, T, bool> comparator)
		{
			for (var i = 0; i < map2.Height; i++)
			for (var j = 0; j < map2.Width; j++)
			{
				if (source.Height <= i + iFrom) return false;
				if (source.Width <= j + jFrom) return false;

				if (!comparator(source[i + iFrom, j + jFrom], map2[i, j]))
					return false;
			}

			return true;
		}

		public M<T> RotateCw()
			=> Init(source.Width, source.Height, p => source[source.Height - p.Y - 1, p.X]);

		public M<T> RotateCcw()
			=> Init(source.Width, source.Height, p => source[p.Y, source.Width - p.X - 1]);
	}

	// todo transpose

	// todo slice
	// separate type can get an indexer for slice
}