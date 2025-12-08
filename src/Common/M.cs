using System.Diagnostics.CodeAnalysis;
using MoreLinq;

namespace Common;

// todo separate wrapper type?
public static class M
{
	public static T[,] Init<T>(int height, int width, Func<V<int>, T> initializer)
	{
		var map = new T[height, width];

		for (var i = 0; i < height; i++)
		for (var j = 0; j < width; j++)
			map[i, j] = initializer(V.Create(i, j));

		return map;
	}

	public static T[,] FromJagged<T>(T[][] jagged)
	{
		if (jagged.Length == 0)
			return new T[0, 0];
		var map = new T[jagged.Length, jagged[0].Length];
		for (var i = 0; i < map.GetLength(0); i++)
		for (var j = 0; j < map.GetLength(1); j++)
			map[i, j] = jagged[i][j];
		return map;
	}

	extension<T>(T[,] source)
	{
		public U[,] Map<U>(Func<V<int>, T, U> mapper)
		{
			return Init(source.GetLength(0), source.GetLength(1), p => mapper(p, source.At(p)));
		}

		public void Iter(Action<V<int>, T> action)
		{
			source.AsEnumerable().ForEach((x) => action(x.point, x.element));
		}
		
		public void Set(V<int> point, T value) => source[point.X, point.Y] = value;

		public bool TrySet(V<int> point, T value)
		{
			if (source.Inside(point))
			{
				source[point.X, point.Y] = value;
				return true;
			}

			return false;
		}

		public IEnumerable<(V<int> point, T element)> AsEnumerable()
		{
			for (var i = 0; i < source.Height(); i++)
			for (var j = 0; j < source.Width(); j++)
				yield return (V.Create(i, j), source[i, j]);
		}

		public T At(V<int> point)
		{
			return source[point.X, point.Y];
		}

		public bool Inside(V<int> point) => TryAt(source, point, out _);

		public Maybe<T> TryAt(V<int> point)
		{
			if (TryAt(source, point, out var result))
				return result;
			return Maybe.None;
		}

		public bool TryAt(V<int> point, [MaybeNullWhen(false)] out T element)
		{
			if (point.X < 0 || point.X >= source.Height()
			                || point.Y < 0 || point.Y >= source.Width())
			{
				element = default;
				return false;
			}

			element = source[point.X, point.Y];
			return true;
		}

		public int Height() => source.GetLength(0);
		public int Width() => source.GetLength(1);

		public bool Compare(T[,] map2)
		{
			if (source.Height() != map2.Height()
			    || source.Width() != map2.Width())
				return false;
			for (var i = 0; i < source.Height(); i++)
			for (var j = 0; j < source.Width(); j++)
			{
				if (!Equals(source[i, j], map2[i, j]))
					return false;
			}

			return true;
		}

		public bool CompareFromSafe(int iFrom, int jFrom, T[,] map2, Func<T, T, bool> comparator)
		{
			for (var i = 0; i < map2.Height(); i++)
			for (var j = 0; j < map2.Width(); j++)
			{
				if (source.Height() <= i + iFrom) return false;
				if (source.Width() <= j + jFrom) return false;

				if (!comparator(source[i + iFrom, j + jFrom], map2[i, j]))
					return false;
			}

			return true;
		}

		public T[,] RotateCw()
			=> Init(source.Width(), source.Height(), p => source[source.Height() - p.Y - 1, p.X]);

		public T[,] RotateCcw()
			=> Init(source.Width(), source.Height(), p => source[p.Y, source.Width() - p.X - 1]);
	}

	// todo: separate type can implement IEnumerable

	// todo slice
	// separate type can get an indexer for slice
}