using System.Collections.Immutable;
using mazharenko.AoCAgent.Generator;
using MoreLinq;

namespace aoc.Year2025;

[BypassNoExamples]
internal partial class Day08
{
	public V3<long>[] Parse(string input)
	{
		return Template.Matching<long, long, long>($"{Numerics.IntegerInt64},{Numerics.IntegerInt64},{Numerics.IntegerInt64}")
			.Select(V3.Create)
			.Lines()
			.Parse(input);
	}
	
	private static ImmutableList<ImmutableHashSet<V3<long>>> InitCircuits(V3<long>[] boxes)
		=> ImmutableList.CreateRange(boxes.Select(ImmutableHashSet.Create));
	
	private static ImmutableList<ImmutableHashSet<V3<long>>> ConnectCircuit(
		ImmutableList<ImmutableHashSet<V3<long>>> circuits,
		(V3<long> a, V3<long> b) connection)
	{
		var (a, b) = connection;
		{
			var circuitA = circuits.First(c => c.Contains(a));
			var circuitB = circuits.First(c => c.Contains(b));
			if (circuitA != circuitB)
			{
				return circuits.Remove(circuitA).Remove(circuitB)
					.Add(circuitA.Union(circuitB));
			}

			return circuits;
		}
	}

	internal partial class Part1
	{
		public long Solve(V3<long>[] input)
		{
			var circuits = InitCircuits(input);
			var connections = input.UniquePairs()
				.OrderBy(x => x.first.Dist(x.second)).Take(1000);

			return connections.Aggregate(circuits, ConnectCircuit)
				.Select(c => (long)c.Count)
				.OrderDescending()
				.Take(3)
				.Aggregate((x, y) => x * y);
		}
	}

	internal partial class Part2
	{
		public long Solve(V3<long>[] input)
		{
			var circuits = InitCircuits(input);
			var connections = input.UniquePairs()
				.OrderBy(x => x.first.Dist(x.second));

			var gg = connections
				.Scan((circuits, connection: ((V3<long> a, V3<long> b)?)null),
					(state, connection) => (ConnectCircuit(state.circuits, connection), connection));

			var (a, b) = gg.First(x => x.circuits.Count == 1).connection!.Value;
			return a.X * b.X;
		}
	}
}