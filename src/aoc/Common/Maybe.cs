namespace aoc.Common;

public class NoneMaybe
{
}

public struct Maybe
{
	public static NoneMaybe None { get; } = new();

	public static Maybe<T> Some<T>(T value) => new(value);
}

public struct Maybe<T> : IEquatable<Maybe<T>>
{
	public static Maybe<T> None = new();

	public bool HasValue { get; }
	private readonly T value;

	public T Value
	{
		get
		{
			if (!HasValue) throw new InvalidOperationException("Maybe wasn't set value");
			return value;
		}
	}

	public Maybe(T value) : this()
	{
		HasValue = true;
		this.value = value;
	}

	public override string ToString()
	{
		return (HasValue ? Value!.ToString() : "None")!;
	}

	// ReSharper disable once UnusedParameter.Global
	public static implicit operator Maybe<T>(NoneMaybe none) => None;

	public static implicit operator Maybe<T>(T value) => new(value);

	public static bool operator ==(Maybe<T> maybeX, T y)
	{
		return maybeX.Equals(y);
	}

	public static bool operator !=(Maybe<T> maybeX, T y)
	{
		return !maybeX.Equals(y);
	}

	public static bool operator ==(T x, Maybe<T> maybeY)
	{
		return maybeY.Equals(x);
	}

	public static bool operator !=(T x, Maybe<T> maybeY)
	{
		return !maybeY.Equals(x);
	}

	public static bool operator ==(Maybe<T> x, Maybe<T> maybeY)
	{
		return maybeY.Equals(x);
	}

	public static bool operator !=(Maybe<T> x, Maybe<T> maybeY)
	{
		return !maybeY.Equals(x);
	}

	public bool Equals(Maybe<T> other)
	{
		if (!HasValue && !other.HasValue) return true;
		if (HasValue != other.HasValue) return false;
		return EqualityComparer<T>.Default.Equals(value, other.value);
	}

	public bool Equals(T other)
	{
		if (!HasValue) return false;
		return EqualityComparer<T>.Default.Equals(value, other);
	}

	public override bool Equals(object? obj)
	{
		switch (obj)
		{
			case Maybe<T> other:
				return Equals(other);
			case T other:
				return Equals(other);
			default:
				return false;
		}
	}

	public override int GetHashCode()
	{
		unchecked
		{
			if (!HasValue) return 0;
			return EqualityComparer<T>.Default.GetHashCode(value!) * 397;
		}
	}
}