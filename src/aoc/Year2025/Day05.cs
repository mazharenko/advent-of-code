namespace aoc.Year2025;

internal partial class Day05
{
	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 3);
		}

		public int Solve((R<long>[] idRanges, long[] ids) input)
		{
			return input.ids.Count(id => input.idRanges.Any(range => range.Contains(id)));
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 14);
		}

		public long Solve((R<long>[] idRanges, long[] ids)  input)
		{
			return R.UnionAll(input.idRanges).Sum(r => r.Len);
		}
	}
	
	public (R<long>[] idRanges, long[] ids) Parse(string input)
	{
		var parser = Template.Matching<long, long>($"{Numerics.IntegerInt64}-{Numerics.IntegerInt64}")
			.Select((from, to) => new R<long>(from, to))
			.Lines()
			.Block()
			.ThenBlock(Numerics.IntegerInt64.Lines());
		return parser.Parse(input);
	}
	
	private readonly Example example = new(
		"""
		3-5
		10-14
		16-20
		12-18

		1
		5
		8
		11
		17
		32
		""");
}