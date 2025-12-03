using aoc.Common;
using ParsingExtensions;

namespace aoc.Year2025;

internal partial class Day02
{
	internal record IdRange(long Start, long End)
	{
		public static IdRange Create(long start, long end) => new(start, end);

		public IEnumerable<long> AsEnumerable()
		{
			for (var i = Start; i <= End; i++)
				yield return i;
		}
	}

	public IdRange[] Parse(string input)
	{
		var parser =
			Template.Matching<long, long>($"{Numerics.IntegerInt64}-{Numerics.IntegerInt64}")
				.Select(IdRange.Create)
				.ManyDelimitedBy(Character.EqualTo(','));

		return parser.Parse(input);
	}

	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 1227775554);
		}

		private static bool IsInvalidId(long id)
		{
			var idS = id.ToString();
			var l = idS.Length;
			return idS[..(l / 2)] == idS[(l / 2)..];
		}

		public long Solve(IdRange[] input)
		{
			var invalidIds =
				input.SelectMany(range => range.AsEnumerable())
					.Where(IsInvalidId);

			return invalidIds.Sum();
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 4174379265);
		}

		private static bool IsInvalidId(long id)
		{
			var idMemory = id.ToString().AsMemory();
			var len = idMemory.Length;
			return (1..(idMemory.Length / 2 + 1))
				.AsEnumerable()
				.Where(size => len % size == 0)
				.Any(size => IsInvalidId(idMemory.Span, size));
		}

		private static bool IsInvalidId(ReadOnlySpan<char> id, int size)
		{
			var first = id[..size];
			for (var i = size; i < id.Length; i += size)
				if (!first.Equals(id[i .. (i + size)], StringComparison.Ordinal))
					return false;
			
			return true;
		}

		public long Solve(IdRange[] input)
		{
			var invalidIds =
				input.SelectMany(range => range.AsEnumerable())
					.Where(IsInvalidId);

			return invalidIds.Sum();
		}
	}

	private readonly Example example =
		new(
			"11-22,95-115,998-1012,1188511880-1188511890," +
			"222220-222224,1698522-1698528,446443-446449,38593856-38593862," +
			"565653-565659,824824821-824824827,2121212118-2121212124"
		);
}