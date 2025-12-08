using MoreLinq;

namespace aoc.Year2024;

internal partial class Day08
{
	private readonly Example example = new(
		"""
		............
		........0...
		.....0......
		.......0....
		....0.......
		......A.....
		............
		............
		........A...
		.........A..
		............
		............
		""");
	public M<char> Parse(string input)
	{
		return Character.AnyChar
			.Map().Parse(input);
	}

	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 14);
		}

		public int Solve(M<char> input)
		{
			var allAntennas = input
				.AsEnumerable()
				.Where(x => x.element != '.')
				.ToLookup(x => x.element, x => x.point);

			return allAntennas.SelectMany(x
					=> x.Cartesian(x, (antenna1, antenna2) =>
						antenna1 == antenna2
							? []
							: new[]
							{
								antenna2 - antenna1 + antenna2,
								antenna1 - antenna2 + antenna1
							}
					)).SelectMany(x => x)
				.Where(input.Inside)
				.Distinct().Count();
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 34);
		}

		public int Solve(M<char> input)
		{
			var allAntennas = input
				.AsEnumerable()
				.Where(x => x.element != '.')
				.ToLookup(x => x.element, x => x.point);

			return allAntennas.SelectMany(x
					=> x.Cartesian(x, (antenna1, antenna2) =>
						antenna1 == antenna2
							? []
							: Enumerable.Range(0, 100).SelectMany(i => new[]
							{
								(antenna2 - antenna1) * i + antenna2,
								(antenna1 - antenna2) * i + antenna1
							})
					)).SelectMany(x => x)
				.Where(input.Inside)
				.Distinct().Count();
		}
	}
}