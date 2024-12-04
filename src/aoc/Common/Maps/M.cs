
namespace aoc.Common.Maps;

// todo separate wrapper type?
public static class M
{
	public static T[,] Init<T>(int height, int width, Func<int, int, T> initializer)
	{
		var map = new T[height, width];

		for (var i = 0; i < height; i++)
			for (var j = 0; j < width; j++)
				map[i, j] = initializer(j, i);

		return map;
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

	// todo slice
	// separate type can get an indexer for slice
}