namespace Common;

public static class Memoization
{
	public static Func<T, TRes> Memoize<T, TRes>(this Func<T, TRes> func) where T : notnull
	{
		var cache = new Dictionary<T, TRes>();
		return x =>
		{
			if (cache.TryGetValue(x, out var res))
				return res;
			res = func(x);
			cache[x] = res;
			return res;
		};
	}

	public static Func<T1, T2, TRes> Memoize<T1, T2, TRes>(this Func<T1, T2, TRes> func)
	{
		var cache = new Dictionary<(T1, T2), TRes>();
		return (x1, x2) =>
		{
			if (cache.TryGetValue((x1, x2), out var res))
				return res;
			res = func(x1, x2);
			cache[(x1, x2)] = res;
			return res;
		};
	}

	public static Func<T, TRes> MemoizeRec<T, TRes>(this Func<Func<T, TRes>, T, TRes> func) where T : notnull
	{
		var cache = new Dictionary<T, TRes>();
		
		return FuncImpl;

		TRes FuncImpl(T x) 
		{
			if (cache.TryGetValue(x, out var res))
				return res;
			res = func(FuncImpl, x);
			cache[x] = res;
			return res;
		}
	}
	public static Func<T1, T2, TRes> MemoizeRec<T1, T2, TRes>(this Func<Func<T1, T2, TRes>, T1, T2, TRes> func)
	{
		var cache = new Dictionary<(T1, T2), TRes>();
		
		return FuncImpl;

		TRes FuncImpl(T1 x1, T2 x2) 
		{
			if (cache.TryGetValue((x1, x2), out var res))
				return res;
			res = func(FuncImpl, x1, x2);
			cache[(x1, x2)] = res;
			return res;
		}
	}
}