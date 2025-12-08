namespace Common.Tests;

public class RTests
{
	[Test]
	public void Union_Overlapping()
	{
		var r1 = new R<int>(1, 5);
		var r2 = new R<int>(3, 7);
		var result = R.Union(r1, r2);
		Assert.That(result, Is.EqualTo(new[] { new R<int>(1, 7) }));
	}

	[Test]
	public void Union_Adjacent()
	{
		var r1 = new R<int>(1, 5);
		var r2 = new R<int>(6, 10);
		var result = R.Union(r1, r2);
		Assert.That(result, Is.EqualTo(new[] { new R<int>(1, 10) }));
	}

	[Test]
	public void Union_NonOverlapping()
	{
		var r1 = new R<int>(1, 5);
		var r2 = new R<int>(7, 10);
		var result = R.Union(r1, r2);
		Assert.That(result, Is.EqualTo(new[] { new R<int>(1, 5), new R<int>(7, 10) }));
	}

	[Test]
	public void Union_Contained()
	{
		var r1 = new R<int>(1, 10);
		var r2 = new R<int>(3, 7);
		var result = R.Union(r1, r2);
		Assert.That(result, Is.EqualTo(new[] { new R<int>(1, 10) }));
	}

	[Test]
	public void UnionAll_Empty()
	{
		var ranges = Array.Empty<R<int>>();
		var result = R.UnionAll(ranges);
		Assert.That(result, Is.Empty);
	}

	[Test]
	public void UnionAll_Single()
	{
		var ranges = new[] { new R<int>(1, 5) };
		var result = R.UnionAll(ranges);
		Assert.That(result, Is.EqualTo(new[] { new R<int>(1, 5) }));
	}

	[Test]
	public void UnionAll_MultipleMergeable()
	{
		var ranges = new[] { new R<int>(1, 5), new R<int>(3, 7), new R<int>(6, 10) };
		var result = R.UnionAll(ranges);
		Assert.That(result, Is.EqualTo(new[] { new R<int>(1, 10) }));
	}

	[Test]
	public void UnionAll_Mixed()
	{
		var ranges = new[] { new R<int>(1, 5), new R<int>(7, 10), new R<int>(4, 8) };
		var result = R.UnionAll(ranges);
		Assert.That(result, Is.EqualTo(new[] { new R<int>(1, 10) }));
	}
}