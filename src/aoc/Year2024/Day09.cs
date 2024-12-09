namespace aoc.Year2024;

internal static class ListExtensions
{
	public static IEnumerable<LinkedListNode<T>> ToEnumerable<T>(this LinkedList<T> list)
	{
		var node = list.First;
		while (node != null)
		{
			yield return node;
			node = node.Next;
		}
	}

	public static IEnumerable<LinkedListNode<T>> ToEnumerable<T>(this LinkedListNode<T> from)
	{
		var node = from;
		while (node != null)
		{
			yield return node;
			node = node.Next;
		}
	}

	public static IEnumerable<LinkedListNode<T>> ToEnumerableBack<T>(this LinkedListNode<T> from)
	{
		var node = from;
		while (node != null)
		{
			yield return node;
			node = node.Previous;
		}
	}
}

internal partial class Day09
{
	private record MemoryBlock(int? Id)
	{
		public required int Length { get; set; }
		public required int Pos { get; set; }
	}

	private static long Checksum(LinkedList<MemoryBlock> memoryList)
	{
		return memoryList
			.Sum(b =>
			{
				if (b.Id.HasValue)
					return b.Id.Value * (((long)2 * b.Pos + b.Length - 1) * b.Length / 2);
				return 0L;
			});
	}

	internal partial class Part1
	{
		private readonly Example example = new("2333133121414131402", 1928);

		public long Solve(int[] input)
		{
			var memoryList = new LinkedList<MemoryBlock>();
			var lenAcc = 0;
			for (var i = 0; i < input.Length; i++)
			{
				if (i % 2 == 0)
					memoryList.AddLast(new MemoryBlock(i / 2) { Length = input[i], Pos = lenAcc });
				else
					memoryList.AddLast(new MemoryBlock(null) { Length = input[i], Pos = lenAcc });

				lenAcc += input[i];
			}

			var firstEmpty =
				memoryList.First!.ToEnumerable().First(n => n.Value.Id is null);
			var lastFile =
				memoryList.Last!.ToEnumerableBack().First(n => n.Value.Id is not null);

			while (lastFile is not null)
			{
				firstEmpty = firstEmpty.ToEnumerable().FirstOrDefault(n =>
					n.Value.Id is null && n.Value.Length > 0 && n.Value.Pos < lastFile.Value.Pos);

				if (firstEmpty is null)
					break;

				var diff = Math.Min(lastFile.Value.Length, firstEmpty.Value.Length);

				memoryList.AddBefore(firstEmpty, new MemoryBlock(lastFile.Value.Id) { Length = diff, Pos = firstEmpty.Value.Pos });

				firstEmpty.Value.Length -= diff;
				firstEmpty.Value.Pos += diff;
				lastFile.Value.Length -= diff;
				lastFile = lastFile.ToEnumerableBack().FirstOrDefault(n => n.Value.Id is not null && n.Value.Length > 0);
			}

			var checksum = Checksum(memoryList);
			return checksum;
		}

		public int[] Parse(string input)
		{
			return
				Character.Digit.Select(c => int.Parse(c.ToString()))
					.Many()
					.Parse(input);
		}
	}

	internal partial class Part2
	{
		private readonly Example example = new("2333133121414131402", 2858);

		public long Solve(int[] input)
		{
			var memoryList = new LinkedList<MemoryBlock>();
			var emptyBlockList = new List<LinkedListNode<MemoryBlock>>();
			var lenAcc = 0;
			for (var i = 0; i < input.Length; i++)
			{
				if (i % 2 == 0)
					memoryList.AddLast(new MemoryBlock(i / 2) { Length = input[i], Pos = lenAcc });
				else
				{
					memoryList.AddLast(new MemoryBlock(null) { Length = input[i], Pos = lenAcc });
					emptyBlockList.Add(memoryList.Last!);
				}

				lenAcc += input[i];
			}

			var last = memoryList.Last;

			while (last is not null)
			{
				var prev = last.Previous;
				if (last.Value.Id is not null)
				{
					var lengthRequired = last.Value.Length;
					var emptyBlockToKeep =
						emptyBlockList.FirstOrDefault(b =>
							b.Value.Pos < last.Value.Pos && b.Value.Length >= lengthRequired);

					if (emptyBlockToKeep is not null)
					{
						emptyBlockToKeep.Value.Length -= last.Value.Length;
						var emptyBlockPos = emptyBlockToKeep.Value.Pos;
						emptyBlockToKeep.Value.Pos += last.Value.Length;

						memoryList.Remove(last);
						memoryList.AddBefore(emptyBlockToKeep, last);
						memoryList.AddAfter(last, new MemoryBlock(null)
						{
							Length = lengthRequired, Pos = last.Value.Pos
						});

						last.Value.Pos = emptyBlockPos;

						if (emptyBlockToKeep.Value.Length is 0)
							emptyBlockList.Remove(emptyBlockToKeep);
					}
				}

				last = prev;
			}

			var checksum = Checksum(memoryList);
			// var checksum = memoryList
			// 	.Aggregate((acc: 0L, pos: 0L), (state, b) =>
			// 	{
			// 		if (b.Id.HasValue)
			// 			return (state.acc + (long)b.Id.Value * ((((long)2*state.pos + b.Length - 1)  ) * b.Length/2), state.pos + b.Length);
			// 		return (state.acc, state.pos + b.Length);
			// 	}).acc;
//			return forCheckSum.Select((x, i) => (long)x * i).Sum();
			return checksum;
		}

		public int[] Parse(string input)
		{
			return
				Character.Digit.Select(c => int.Parse(c.ToString()))
					.Many()
					.Parse(input);
		}
	}
}