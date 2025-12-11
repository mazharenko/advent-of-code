namespace aoc.Year2025;

internal partial class Day11
{
	private static long CountPaths(string from, string to, (string from, string[] to)[] input)
	{
		var countPathsMemoizable = CountPathsMemoizable;
		return countPathsMemoizable.MemoizeRec()(from, to);

		long CountPathsMemoizable(Func<string, string, long> countFunc, string from1, string to1)
		{
			if (from1 == to1) return 1;

			return input.Where(x => x.to.Contains(to1))
				.Select(x => x.from)
				.Sum(neighbor => countFunc(from1, neighbor));
		}
	}

	internal partial class Part1
	{
		public long Solve((string from, string[] to)[] input)
		{
			return CountPaths("you", "out", input);
		}

		private readonly Example example = new(
			"""
			aaa: you hhh
			you: bbb ccc
			bbb: ddd eee
			ccc: ddd eee fff
			ddd: ggg
			eee: out
			fff: out
			ggg: out
			hhh: ccc fff iii
			iii: out
			""", 5);
	}


	internal partial class Part2
	{
		public long Solve((string from, string[] to)[] input)
		{
			return CountPaths("svr", "fft", input)
			       * CountPaths("fft", "dac", input)
			       * CountPaths("dac", "out", input)
			       + CountPaths("svr", "dac", input)
			       * CountPaths("dac", "fft", input)
			       * CountPaths("fft", "out", input);
		}

		private readonly Example example = new(
			"""
			svr: aaa bbb
			aaa: fft
			fft: ccc
			bbb: tty
			tty: ccc
			ccc: ddd eee
			ddd: hub
			hub: fff
			eee: dac
			dac: fff
			fff: ggg hhh
			ggg: out
			hhh: out
			""", 2);
	}

	public (string from, string[] to)[] Parse(string input)
	{
		return Template.Matching<string, string[]>(
			$"{Character.Letter.Many().Text()}: {Character.Letter.Many().Text().ManyDelimitedBySpaces()}"
		).Lines().Parse(input);
	}
}