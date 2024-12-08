using System.Diagnostics.CodeAnalysis;
using MoreLinq;

namespace aoc.Common;

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

	public static U[,] Map<T, U>(this T[,] source, Func<V<int>, T, U> mapper)
	{
		return Init(source.GetLength(0), source.GetLength(1), p => mapper(p, source.At(p)));
	}

	public static void Iter<T>(this T[,] source, Action<V<int>, T> action)
	{
		source.AsEnumerable().ForEach((x) => action(x.point, x.element));
	}

	// todo: separate type can implement IEnumerable
	public static IEnumerable<(V<int> point, T element)> AsEnumerable<T>(this T[,] source)
	{
		for (var i = 0; i < source.Height(); i++)
		for (var j = 0; j < source.Width(); j++)
			yield return (V.Create(i, j), source[i, j]);
	}

	public static T At<T>(this T[,] source, V<int> point)
	{
		return source[point.X, point.Y];
	}
	
	public static bool Inside<T>(this T[,] source, V<int> point) => TryAt(source, point, out _);
	
	// todo: maybe?
	public static bool TryAt<T>(this T[,] source, V<int> point, [MaybeNullWhen(false)] out T element)
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

	public static int Height<T>(this T[,] map) => map.GetLength(0);
	public static int Width<T>(this T[,] map) => map.GetLength(1);

	public static bool Compare<T>(this T[,] map1, T[,] map2)
	{
		if (map1.Height() != map2.Height()
		    || map1.Width() != map2.Width())
			return false;
		for (var i = 0; i < map1.Height(); i++)
		for (var j = 0; j < map1.Width(); j++)
		{
			if (!Equals(map1[i, j], map2[i, j]))
				return false;
		}

		return true;
	}

	public static bool CompareFromSafe<T>(this T[,] map1, int iFrom, int jFrom, T[,] map2, Func<T, T, bool> comparator)
	{
		for (var i = 0; i < map2.Height(); i++)
		for (var j = 0; j < map2.Width(); j++)
		{
			if (map1.Height() <= i + iFrom) return false;
			if (map1.Width() <= j + jFrom) return false;

			if (!comparator(map1[i + iFrom, j + jFrom], map2[i, j]))
				return false;
		}

		return true;
	}

	public static T[,] RotateCw<T>(this T[,] map)
		=> Init(map.Width(), map.Height(), p => map[map.Height() - p.Y - 1, p.X]);

	public static T[,] RotateCcw<T>(this T[,] map)
		=> Init(map.Width(), map.Height(), p => map[p.Y, map.Width() - p.X - 1]);

	// todo slice
	// separate type can get an indexer for slice
}