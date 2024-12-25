using aoc.Common;
using MoreLinq;

namespace aoc.Year2024;

internal partial class Day25
{
	internal partial class Part1
	{
		private readonly Example example = new(
			"""
			#####
			.####
			.####
			.####
			.#.#.
			.#...
			.....

			#####
			##.##
			.#.##
			...##
			...#.
			...#.
			.....

			.....
			#....
			#....
			#...#
			#.#.#
			#.###
			#####

			.....
			.....
			#.#..
			###..
			###.#
			###.#
			#####

			.....
			.....
			.....
			#....
			#.#..
			#.#.#
			#####
			""", 3);

		public int Solve(char[][][] input)
		{
			var locks = input.Where(map =>
				map[0].All(c => c == '#'));

			var keys = input.Where(map =>
				map[^1].All(c => c == '#'));

			var lockPinsSeq = locks.Select(
				@lock => @lock.Transpose().Select(x => x.Count(c => c == '#')).ToArray()
			);

			var keysPinsSeq = keys.Select(
				@lock => @lock.Transpose().Select(x => x.Count(c => c == '#')).ToArray()
			);

			return lockPinsSeq.Cartesian(keysPinsSeq,
					(lockPins, keysPins) => lockPins.Zip(keysPins))
				.Count(x => x.All(pins => pins.First + pins.Second <= 7));
		}

		public char[][][] Parse(string input)
		{
			return Character.In('#', '.')
				.MapJagged()
				.Blocks().Parse(input);
		}
	}

	internal partial class Part2
	{
		public string Solve(string input)
		{
			throw new NotImplementedException();
		}
	}
}