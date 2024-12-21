using mazharenko.AoCAgent.Generator;
using MoreLinq;

namespace aoc.Year2024;

[BypassNoExamples]
internal partial class Day21
{
	private static long CountMinSequence(char[] keys, int level, int levels, Dictionary<(string, int), long> counts)
	{
		if (level == levels) return keys.Length;
		var pad = level is 0 ? (IPad)new NumPad() : new ArrowPad();

		if (counts.TryGetValue((new string(keys), level), out var count))
			return count;

		var res = keys.Prepend('A').Pairwise(pad.Routes)
			.Select(routes =>
				routes.Select(route =>
					CountMinSequence(route.Append('A').ToArray(), level + 1, levels, counts)).Min()
			).Sum();
		
		counts[(new string(keys), level)] = res;
		return res;
	}

	internal partial class Part1
	{
		public long Solve(char[][] input)
		{
			return input.Select(l =>
				CountMinSequence(l, 0, 3, new Dictionary<(string, int), long>())
				* int.Parse(new string(l.Where(char.IsDigit).ToArray()))
			).Sum();
		}
	}

	internal partial class Part2
	{
		public long Solve(char[][] input)
		{
			return input.Select(l =>
				CountMinSequence(l, 0, 26, new Dictionary<(string, int), long>())
				* long.Parse(new string(l.Where(char.IsDigit).ToArray()))
			).Sum();
		}
	}

	public char[][] Parse(string input)
	{
		return Character.LetterOrDigit.Many()
			.Lines().Parse(input);
	}

	private interface IPad
	{
		IEnumerable<char[]> Routes(char from, char to);
	}

	private class ArrowPad : IPad
	{
		public IEnumerable<char[]> Routes(char from, char to)
		{
			var dcol = Col(to) - Col(from);
			var drow = Row(to) - Row(from);
			if (Col(from) is not 0 || Row(to) is not 1)
				yield return Enumerable.Repeat(drow > 0 ? '^' : 'v', Math.Abs(drow))
					.Concat(Enumerable.Repeat(dcol > 0 ? '>' : '<', Math.Abs(dcol)))
					.ToArray();
			if (Row(from) is not 1 || Col(to) is not 0)
				yield return Enumerable.Repeat(dcol > 0 ? '>' : '<', Math.Abs(dcol))
					.Concat(Enumerable.Repeat(drow > 0 ? '^' : 'v', Math.Abs(drow)))
					.ToArray();
			yield break;

			int Col(char c) =>
				c switch
				{
					'<' => 0,
					'^' or 'v' => 1,
					_ => 2
				};

			int Row(char c) =>
				c switch
				{
					'^' or 'A' => 1,
					_ => 0
				};
		}
	}

	private class NumPad : IPad
	{
		public IEnumerable<char[]> Routes(char from, char to)
		{
			var dcol = Col(to) - Col(from);
			var drow = Row(to) - Row(from);

			if (Col(to) is not 0 || Row(from) is not 0)
				yield return Enumerable.Repeat(dcol > 0 ? '>' : '<', Math.Abs(dcol))
					.Concat(Enumerable.Repeat(drow > 0 ? '^' : 'v', Math.Abs(drow)))
					.ToArray();

			if (Col(from) is not 0 || Row(to) is not 0)
				yield return Enumerable.Repeat(drow > 0 ? '^' : 'v', Math.Abs(drow))
					.Concat(Enumerable.Repeat(dcol > 0 ? '>' : '<', Math.Abs(dcol)))
					.ToArray();
			yield break;

			int Col(char c) => c switch
			{
				'0' => 1,
				'A' => 2,
				_ => (Convert.ToInt32(c.ToString()) - 1) % 3
			};

			int Row(char c) => c is 'A' or '0' ? 0 : (Convert.ToInt32(c.ToString()) - 1) / 3 + 1;
		}
	}
}