using aoc.Common;
using MoreLinq;

namespace aoc.Year2024;

// чтобы и парсинг переиспользовать, звучит как надо базовый класс или сам Day становится базовым классом
// но надо как-то разделять, когда тип один и когда два разных.
// и туда же можно засунуть примеры. но потом как-то красиво навесить им ожидания... неужели конструктор

// ну мб все же продублировать примеры в Part, но какие-то без ожиданий, и потом в конструкторе их промутировать,
// проставив им ожидание. а так или просто другая реализация IExample?

// как нагенерить его в GetExamples - ну поглядим
internal partial class Day08
{
	public char[,] Parse(string input)
	{
		return Character.LetterOrDigit.Or(Character.EqualTo('.'))
			.Map().Parse(input);
	}

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