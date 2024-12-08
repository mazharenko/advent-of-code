using aoc.Common;

namespace aoc.Year2024;

internal partial class Day04
{
	public char[,] Parse(string input)
	{
		return Character.Letter.Map().Parse(input);
	}

	private static int CountFormations(char[,] input, char[][,] formations)
	{
		return input.AsEnumerable()
			.Sum(x =>
				formations.Count(xmas => input.CompareFromSafe(x.point.X, x.point.Y, xmas, (c, xmasc) => xmasc == '.' || xmasc == c))
			);
	}

	internal partial class Part1
	{
		private readonly Example example = new(
			"""
			MMMSXXMASM
			MSAMXMSMSA
			AMXSXMAAMM
			MSAMASMSMX
			XMASAMXAMM
			XXAMMXXAMA
			SMSMSASXSS
			SAXAMASAAA
			MAMMMXMMMM
			MXMXAXMASX
			""", 18);

		public int Solve(char[,] input)
		{
			var xmasFormations = new[]
				{
					new[]
					{
						"XMAS".ToCharArray(),
					},
					new[]
					{
						"X...".ToCharArray(),
						".M..".ToCharArray(),
						"..A.".ToCharArray(),
						"...S".ToCharArray()
					}
				}.Select(M.FromJagged)
				.SelectMany(map => new[] { map, map.RotateCw(), map.RotateCcw(), map.RotateCw().RotateCw() })
				.ToArray();


			return CountFormations(input, xmasFormations);
		}
	}

	internal partial class Part2
	{
		private readonly Example example = new(
			"""
			MMMSXXMASM
			MSAMXMSMSA
			AMXSXMAAMM
			MSAMASMSMX
			XMASAMXAMM
			XXAMMXXAMA
			SMSMSASXSS
			SAXAMASAAA
			MAMMMXMMMM
			MXMXAXMASX
			""", 9);

		public int Solve(char[,] input)
		{
			var xMas =
				M.FromJagged([
					"M.S".ToCharArray(),
					".A.".ToCharArray(),
					"M.S".ToCharArray()
				]);
			var xmasFormations = new[]
			{
				xMas, xMas.RotateCw(), xMas.RotateCcw(), xMas.RotateCw().RotateCw()
			};

			return CountFormations(input, xmasFormations);
		}
	}
}