using aoc.Common.Maps;

namespace aoc.Year2024.Day04;

internal partial class Day04
{
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
			// todo generate? rotations?
			var xmasFormations = new[]
			{
				"XMAS",
				
				"""
				X...
				.M..
				..A.
				...S
				""",
				
				"""
				X
				M
				A
				S
				""",
				
				"""
				...X
				..M.
				.A..
				S...
				""",
				
				"SAMX",
				
				"""
				S...
				.A..
				..M.
				...X
				""",
				
				"""
				S
				A
				M
				X
				""",
				
				"""
				...S
				..A.
				.M..
				X...
				"""
			}.Select(Character.Letter.Or(Character.EqualTo('.')).Map().Parse).ToArray();

			var count = 0; // todo functional. map, fold?
			for (var i = 0; i < input.Height(); i++)
			for (var j = 0; j < input.Width(); j++)
			{
				count +=
					xmasFormations.Count(xmas =>
						input.CompareFromSafe(i, j, xmas, (c, xmasc) => xmasc == '.' || xmasc == c))
					;
			}

			return count;
		}

		public char[,] Parse(string input)
		{
			return Character.Letter.Map().Parse(input);
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
			// todo generate? rotations?
			var xmasFormations = new[]
			{
				"""
				M.S
				.A.
				M.S
				""",
				
				"""
				M.M
				.A.
				S.S
				""",
				
				"""
				S.M
				.A.
				S.M
				""",
				
				"""
				S.S
				.A.
				M.M
				""",
			}.Select(Character.Letter.Or(Character.EqualTo('.')).Map().Parse).ToArray();

			var count = 0; // todo functional. map, fold?
			for (var i = 0; i < input.Height(); i++)
			for (var j = 0; j < input.Width(); j++)
			{
				count +=
					xmasFormations.Count(xmas =>
						input.CompareFromSafe(i, j, xmas, (c, xmasc) => xmasc == '.' || xmasc == c))
					;
			}

			return count;
		}

		public char[,] Parse(string input)
		{
			return Character.Letter.Map().Parse(input);
		}
	}
}