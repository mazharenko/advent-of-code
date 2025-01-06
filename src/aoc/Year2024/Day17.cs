using aoc.Common;
using mazharenko.AoCAgent.Generator;
using MoreLinq;
using ParsingExtensions;

namespace aoc.Year2024;

internal partial class Day17
{
	private readonly Example example = new(
		"""
		Register A: 729
		Register B: 0
		Register C: 0

		Program: 0,1,5,4,3,0
		""");

	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, "4,6,3,5,6,3,5,2,1,0");
		}

		public string Solve((Registers registers, byte[] program) input)
		{
			var output = RunProgram(input.registers, input.program);
			return string.Join(",", output);
		}
	}

	[BypassNoExamples]
	internal partial class Part2
	{
		public long Solve((Registers registers, byte[] program) input)
		{
			// not common solution, relies on what the inputs look like.
			// generally we know about the program that:
			// 1. jnz command generally just implements a loop over the program
			// 2. nothing depends on what state registers B and C are in at the beginning of an iteration
			// 3. each iteration outputs a single value
			// 4. every iteration A is divided by 8
			// So this solution is basically bruteforce but in many stages.
			// First we find what A value we need to have the last number of the program printed.
			// Then we find what A value we need to have the last two numbers of the program printed, but 
			// start from the previously found A multiplied by 8.
			// Then we find what A value we need to have the last three numbers of the program printed, but 
			// start from the previously found A multiplied by 8.
			// And so on.
			var (_, program) = input;

			var outputLength = 1;
			var a = 0L;
			while (true)
			{
				if (RunProgram(new Registers(a, 0L, 0L), program).SequenceEqual(program[^outputLength..]))
				{
					if (outputLength == program.Length)
						return a;
					outputLength++;
					a <<= 3;
				}
				else a++;
			}
		}
	}

	public (Registers registers, byte[] program) Parse(string input)
	{
		return
			Template.Matching<int>($"Register A: {Numerics.IntegerInt32}")
				.ThenLine(Template.Matching<int>($"Register B: {Numerics.IntegerInt32}"))
				.ThenLine(Template.Matching<int>($"Register C: {Numerics.IntegerInt32}"))
				.Block()
				.Then(
					Span.EqualTo("Program: ")
						.IgnoreThen(Numerics.IntegerInt32.Select(i => (byte)i)
							.ManyDelimitedBy(Character.EqualTo(',')
							)
						))
				.Select(x =>
				{
					var (((a, b), c), program) = x;
					return (new Registers(a, b, c), program);
				})
				.Parse(input);
	}

	internal record Registers(long A, long B, long C);

	private static List<byte> RunProgram(Registers registers, byte[] program)
	{
		var (rA, rB, rC) = registers;

		var chunks = program.Chunk(2).ToList();
		var output = new List<byte>();
		for (var i = 0; i < chunks.Count; i++)
		{
			var command = chunks[i][0];
			var operand = chunks[i][1];

			switch (command)
			{
				case 0: // adv
					rA >>= (int)ResolveCombo(operand);
					break;
				case 1: // bxl
					rB ^= operand;
					break;
				case 2: // bst
					rB = ResolveCombo(operand) % 8;
					break;
				case 3: // jnz
					if (rA is not 0) i = operand / 2 - 1;
					break;
				case 4: // bxc
					rB ^= rC;
					break;
				case 5: // out
					output.Add((byte)(ResolveCombo(operand) % 8L));
					break;
				case 6: // bdv
					rB = rA >> (int)ResolveCombo(operand);
					break;
				case 7: // cdv
					rC = rA >> (int)ResolveCombo(operand);
					break;
			}
		}

		return output;

		long ResolveCombo(byte operand)
		{
			return operand switch
			{
				<= 3 => operand,
				4 => rA,
				5 => rB,
				6 => rC,
				_ => throw new ArgumentOutOfRangeException(nameof(operand), operand, null)
			};
		}
	}
}