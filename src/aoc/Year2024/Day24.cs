using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using mazharenko.AoCAgent.Generator;
using aoc.Common;
using Spectre.Console;

namespace aoc.Year2024;

internal partial class Day24
{
	public record InputBit(string Name, int Digit, int Value) // bool?
	{
		public string FullName { get; } = $"{Name}{Digit:00}";
	}

	public enum Operation
	{
		Xor,
		Or,
		And
	}

	public record Gate(string Output, string Input1, string Input2, Operation Operation)
	{
		public string Output { get; set; } = Output;
	}

	public (InputBit[], Gate[]) Parse(string input)
	{
		var wireName = Character.LetterOrDigit.Many().Select(chars => new string(chars));
		var op = Character.Letter.Many().Select(
			chars => new string(chars) switch
			{
				"OR" => Operation.Or,
				"XOR" => Operation.Xor,
				"AND" => Operation.And
			}
		);

		var parser =
			Character.Letter.Then(Numerics.IntegerInt32)
				.ThenIgnore(Span.EqualTo(": "))
				.Then(Numerics.IntegerInt32)
				.Select(x => new InputBit(x.Item1.Item1.ToString(), x.Item1.Item2, x.Item2))
				.Lines()
				.Block()
				.ThenBlock(
					wireName.ThenIgnore(SpanX.Space).Then(op).ThenIgnore(SpanX.Space).Then(wireName).ThenIgnore(Span.EqualTo(" -> ")).Then(wireName)
						.Select(((tuple, s) => // todo: tuple flatten? flattening Select? flattening Then?
							{
								return new Gate(s, tuple.Item1.Item1, tuple.Item2, tuple.Item1.Item2);
							})
						).Lines());

		return parser.Parse(input);
	}

	public IDictionary<string, (string, string, string)> Parse2(string input)
	{
		var wire = Character.LetterOrDigit.Many().Select(chars => new string(chars));
		var op = Character.Letter.Many().Select(
			chars => new string(chars)
		);

		var dic = new Dictionary<string, (string, string, string)>();

		var parser = wire.ThenIgnore(Span.EqualTo(": "))
			.Then(Numerics.IntegerInt32)
			.Select((s, i) => (s, ("", "", "")))
			.Lines()
			.Block()
			.ThenBlock(
				wire.ThenIgnore(SpanX.Space).Then(op).ThenIgnore(SpanX.Space).Then(wire).ThenIgnore(Span.EqualTo(" -> ")).Then(wire)
					.Select(((tuple, s) => { return (s, (tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item2)); })
					).Lines());

		var (s1, s2) = parser.Parse(input);

		foreach (var (s, lazy) in s1) dic[s] = lazy;
		foreach (var (s, lazy) in s2) dic[s] = lazy;

		return dic;
	}

	internal partial class Part1
	{
		private readonly Example example = new(
			"""
			x00: 1
			x01: 1
			x02: 1
			y00: 0
			y01: 1
			y02: 0

			x00 AND y00 -> z00
			x01 XOR y01 -> z01
			x02 OR y02 -> z02
			""", 4);

		public long Solve((InputBit[] inputBits, Gate[] gates) input)
		{
			var lazyGates =
				input.inputBits.ToDictionary(inputBit => inputBit.FullName, inputBit => new Lazy<int>(inputBit.Value));

			foreach (var gate in input.gates)
				lazyGates.Add(gate.Output, gate.Operation switch
				{
					Operation.And => new Lazy<int>(() => lazyGates[gate.Input1].Value & lazyGates[gate.Input2].Value),
					Operation.Or => new Lazy<int>(() => lazyGates[gate.Input1].Value | lazyGates[gate.Input2].Value),
					Operation.Xor => new Lazy<int>(() => lazyGates[gate.Input1].Value ^ lazyGates[gate.Input2].Value)
				});

			var zeroLazy = new Lazy<int>(0);
			var z = (..64).AsEnumerable()
				.Aggregate(0L, (acc, digit) =>
					acc | ((long)lazyGates.GetValueOrDefault($"z{digit:00}", zeroLazy).Value << digit));

			return z;
		}
	}

	[BypassNoExamples]
	internal partial class Part2
	{
		public string Solve((InputBit[] inputBits, Gate[] gates) input)
		{
			var gates = input.gates;

			var swapped = new List<string>(); 
			
			// Assume some adder structure and that outputs are swapped within a single bit computation
			var carryOuts = new Dictionary<int, string>
			{
				{ 0, FindGate("x00", "y00", Operation.And).Output },
			};
			
			for (var i = 1; i < 45; i++)
			{
				// xi ^ yi
				var xor  = FindGate($"x{i:00}", $"y{i:00}", Operation.Xor)!;
				// xi & yi
				var and = FindGate($"x{i:00}", $"y{i:00}", Operation.And)!;

				var carryIn = carryOuts[i - 1];

				// carry in & (xi ^ yi)
				// single (carry in & A) is expected, and A must be (xi ^ yi) output
				var carryOut1 = FindGateOneOperand(carryIn, Operation.And, out var carryOut1Operand)!;
				if (carryOut1Operand != xor.Output) // xor has wrong output
				{
					Swap(xor.Output, carryOut1Operand);
					i--;
					continue;
				}
				
				// carry in ^ (xi ^ yi)
				// single (carry in ^ A) is expected, and A must be (xi ^ yi) output
				var zi = FindGateOneOperand(carryIn, Operation.Xor, out var ziOperand);
				if (ziOperand != xor.Output)
				{
					Swap(xor.Output, ziOperand);
					i--;
					continue;
				}

				if (zi.Output != $"z{i:00}")
				{
					Swap(zi.Output, $"z{i:00}");
					i--;
					continue;
				}

				// (carry in ^ (xi ^ yi)) | (xi & yi)
				// assume this one is correct, because it depends on two outputs that could've been swapped
				var carryOut = FindGate(carryOut1.Output, and.Output, Operation.Or);

				carryOuts[i] = carryOut.Output;
			}

			return string.Join(",", swapped.Order());

			void Swap(string output1, string output2)
			{
				var gate1 = gates.Single(g => g.Output == output1);
				var gate2 = gates.Single(g => g.Output == output2);
				(gate1.Output, gate2.Output) = (gate2.Output, gate1.Output);
				swapped.Add(output1);
				swapped.Add(output2);
			}

			Gate FindGateOneOperand(string operand1, Operation operation, out string operand2)
			{
				var gate = gates.First(gate =>
					gate.Input1 == operand1 && gate.Operation == operation
					|| gate.Input2 == operand1 && gate.Operation == operation);
				
				operand2 = gate.Input1 == operand1 ? gate.Input2 : gate.Input1;
				return gate;
			}

			Gate FindGate(string operand1, string operand2, Operation operation)
			{
				return gates.First(gate =>
					gate.Input1 == operand1 && gate.Input2 == operand2 && gate.Operation == operation
					|| gate.Input1 == operand2 && gate.Input2 == operand1 && gate.Operation == operation);
			}
		}
	}
}