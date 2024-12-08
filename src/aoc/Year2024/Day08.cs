using aoc.Common;
using MoreLinq;

namespace aoc.Year2024;

internal partial class Day08
{
	internal partial class Part1
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
			""", 14);

		public int Solve(char[,] input)
		{
			var allAntennas = input
				.AsEnumerable()
				.Where(x => x.element != '.')
				.ToLookup(x => x.element, x => x.point);

			return allAntennas.SelectMany(x
				=> x.Cartesian(x, (antenna1, antenna2) => 
					antenna1 == antenna2 ? [] : new[]
					{
						antenna2 - antenna1 + antenna2,
						antenna1 - antenna2 + antenna1
					}
				)).SelectMany(x => x)
				.Where(input.Inside)
				.Distinct().Count();
		}

		public char[,] Parse(string input)
		{
			return Character.LetterOrDigit.Or(Character.EqualTo('.')).Map().Parse(input);
		}
	}

	internal partial class Part2
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
			""", 34);
		
		public int Solve(char[,] input)
		{
			var allAntennas = input
         				.AsEnumerable()
         				.Where(x => x.element != '.')
         				.ToLookup(x => x.element, x => x.point);

			return allAntennas.SelectMany(x
					=> x.Cartesian(x, (antenna1, antenna2) =>
						antenna1 == antenna2
							? []
							: Enumerable.Range(0, 100).SelectMany(i => new []
							{
								(antenna2 - antenna1) * i + antenna2,
								(antenna1 - antenna2) * i + antenna1
							})
					)).SelectMany(x => x)
				.Where(input.Inside)
				.Distinct().Count();
		}

		public char[,] Parse(string input)
		{
			return Character.LetterOrDigit.Or(Character.EqualTo('.')).Map().Parse(input);
		}
	}
}