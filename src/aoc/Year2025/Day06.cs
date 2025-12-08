using MoreLinq.Extensions;

namespace aoc.Year2025;

internal partial class Day06
{
	public enum Op { Mul, Add }
	
	private readonly TextParser<Op> opParser = 
		Character.EqualTo('*').Value(Op.Mul).Or(Character.EqualTo('+').Value(Op.Add));

	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 4277556);
		}

		public (long[] numbers, Op op)[] Parse(string input)
		{
			var lines = SpanX.Any.Text().Lines().Parse(input);
			
			// 123 328  51 64         123  45   6
			//  45 64  387 23    ->   328  64  98
			//   6 98  215 314         51 387 215
			//                         64  23 314
			var numberCols = // todo M.Slice and M.Transpose
				lines[..^1].Select(line => Numerics.IntegerInt64.ManyDelimitedBySpaces().Parse(line.Trim()))
					.Transpose()
					.Select(row => row.ToArray())
					.ToArray();

			var ops = opParser.ManyDelimitedBySpaces().Parse(lines[^1].Trim());
			
			return numberCols.Zip(ops).ToArray();
		}

		public long Solve((long[] numbers, Op op)[] input)
		{
			return input.Sum(x =>
				x.numbers.Aggregate((n1, n2) =>
					x.op switch { Op.Mul => n1 * n2, Op.Add => n1 + n2 }
				)
			);
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, 3263827);
		}

		public (long[] numbers, Op op)[] Parse(string input)
		{
			var map = Character.AnyChar.MapJagged().Parse(input);
				
			// 123 328  51 64          1  
			//  45 64  387 23    ->    24 
			//   6 98  215 314         356
			//                            
			//                         369
			//                         248
			//                         8  
			//                            
			//                          32
			//                         581
			//                         175
			//                            
			//                         623
			//                         431
			//                           4
			var numberCols = map[..^1].Transpose() // todo M.Slice and M.Transpose
				.Select(r => new string(r.ToArray()).Trim())
				.Split(string.IsNullOrEmpty)
				.Select(c => c.Select(long.Parse).ToArray())
				.ToArray();

			var ops = opParser.ManyDelimitedBySpaces().Parse(new string(map[^1]).Trim());

			return numberCols.Zip(ops).ToArray();
		}

		public long Solve((long[] numbers, Op op)[] input)
		{
			return input.Sum(x =>
				x.numbers.Aggregate((n1, n2) =>
					x.op switch { Op.Mul => n1 * n2, Op.Add => n1 + n2 }
				)
			);
		}
	}

	private readonly Example example = new(
		"""
		123 328  51 64 
		 45 64  387 23 
		  6 98  215 314
		*   +   *   +  
		""");

}