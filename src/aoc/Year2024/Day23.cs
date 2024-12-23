namespace aoc.Year2024;

internal partial class Day23
{
	private IList<HashSet<string>> CollectGroups((string from, string to)[] input)
	{
		var d =
			input.Concat(input.Select(x => (x.to, x.from)))
				.GroupBy(x => x.Item1, x => x.Item2)
				.ToDictionary(x => x.Key, x => x.ToHashSet());

		var all = input.SelectMany(x => new[] { x.from, x.to })
			.ToHashSet();

		var groups = new List<HashSet<string>>();
		foreach (var comp in all)
		{
			var matchingGroups = groups.Where(
					group => group.IsSubsetOf(d[comp]))
				.ToList();

			foreach (var matchingGroup in matchingGroups)
				groups.Add([..matchingGroup, comp]);

			groups.Add([comp]);
		}

		return groups;
	}

	internal partial class Part1
	{
		public Part1()
		{
			Expect(example, 7);
		}

		public int Solve((string to, string from)[] input)
		{
			return CollectGroups(input)
				.Count(group => group.Count == 3 && group.Any(g => g.StartsWith('t')));
		}
	}

	internal partial class Part2
	{
		public Part2()
		{
			Expect(example, "co,de,ka,ta");
		}

		public string Solve((string from, string to)[] input)
		{
			var groups = CollectGroups(input);
			return string.Join(",", groups.MaxBy(group => group.Count)!.Order());
		}
	}

	public (string, string)[] Parse(string input)
	{
		return Character.Letter.Many()
			.ThenIgnore(Span.EqualTo("-"))
			.Then(Character.Letter.Many())
			.Select(x => (new string(x.Item1), new string(x.Item2)))
			.Lines().Parse(input);
	}

	private readonly Example example = new(
		"""
		kh-tc
		qp-kh
		de-cg
		ka-co
		yn-aq
		qp-ub
		cg-tb
		vc-aq
		tb-ka
		wh-tc
		yn-cg
		kh-ub
		ta-co
		de-co
		tc-td
		tb-wq
		wh-td
		ta-ka
		td-qp
		aq-cg
		wq-ub
		ub-vc
		de-ta
		wq-aq
		wq-vc
		wh-yn
		ka-de
		kh-ta
		co-tc
		wh-qp
		tb-vc
		td-yn
		""");
}