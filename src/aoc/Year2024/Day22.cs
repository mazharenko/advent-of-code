using MoreLinq;

namespace aoc.Year2024;

internal partial class Day22
{
	public long[] Parse(string input)
	{
		return Numerics.IntegerInt64.Lines().Parse(input);
	}

	private static IEnumerable<long> SecretSeq(long init)
	{
		return MoreEnumerable.Generate(init, current =>
		{
			current = ((current * 64) ^ current) % 16777216;
			current = ((current / 32) ^ current) % 16777216;
			current = ((current * 2048) ^ current) % 16777216;
			return current;
		});
	}

	internal partial class Part1
	{
		private readonly Example example = new(
			"""
			1
			10
			100
			2024
			""", 37327623);

		public long Solve(long[] input)
		{
			return input.Select(n => SecretSeq(n).ElementAt(2000)).Sum();
		}
	}

	internal partial class Part2
	{
		private readonly Example example = new(
			"""
			1
			2
			3
			2024
			""", 23L);

		public long Solve(long[] input)
		{
			return input.Select(buyer => SecretSeq(buyer).Take(2001))
				.SelectMany(CollectPricesAndChanges)
				.GroupBy(x => x.changeSeq, x => x.price, (_, prices) => prices.Sum())
				.Max();
		}

		private static IEnumerable<((long, long, long, long) changeSeq, long price)> CollectPricesAndChanges(IEnumerable<long> secret)
		{
			return secret.Select(x => x % 10)
				.Window(5)
				.Select(window => (
					changeSeq: (window[1] - window[0], window[2] - window[1], window[3] - window[2], window[4] - window[3]),
					price: window[4]
				)).DistinctBy(x => x.changeSeq);
		}
	}
}