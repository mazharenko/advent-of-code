using System.Collections;

namespace aoc.Common;

public static class L
{
	public static L<T> Empty<T>() => L<T>.Empty;
	public static L<T> Create<T>(T value, L<T>? tail) => L<T>.Create(value, tail);
	public static L<T> Singleton<T>(T value) => Create(value, null);
	public static L<T> Create<T>(IList<T> values) => L<T>.Create(values);
} 

public record L<T> : IEnumerable<T>
{
	private readonly T? value;
	private readonly L<T>? tail;
	private readonly bool empty;

	private L(T? value, L<T>? tail, bool empty)
	{
		this.value = value;
		this.tail = tail;
		this.empty = empty;
	}

	public static L<T> Empty { get; } = new(default, null, true);
	
	public static L<T> Create(T value, L<T>? tail) => new(value, tail, false);

	public static L<T> Create(IList<T> values)
	{
		if (values.Count == 0)
			return Empty;
		var tmp = Create(values.Last(), null);
		for (var i = values.Count - 2; i >= 0; i--)
		{
			tmp = Create(values[i], tmp);
		}
		return tmp;
	}
	
	public IEnumerator<T> GetEnumerator()
	{
		if (empty) yield break;
		var current = this;
		while (current is not null)
		{
			yield return current.value!;
			current = current.tail;
		}
	}
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


	public L<T> Prepend(T head) => new(head, empty ? null : this, false);

	public L<T> Append(L<T> newTail)
	{
		if (newTail.empty) return this;
		return L.Create(this.AsEnumerable().Concat(newTail.AsEnumerable()).ToList());
	}

	public T Head => empty ? throw new InvalidOperationException("List is empty") : value!;
	public L<T>? Tail => tail;
	
	public bool IsEmpty => empty;

	public void Deconstruct(out T head, out L<T>? tail1)
	{
		if (empty) throw new InvalidOperationException("List is empty");
		head = value!;
		tail1 = tail;
	}

	public virtual bool Equals(L<T>? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		return EqualityComparer<T?>.Default.Equals(value, other.value) && Equals(tail, other.tail) && empty == other.empty;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(value, tail, empty);
	}
}