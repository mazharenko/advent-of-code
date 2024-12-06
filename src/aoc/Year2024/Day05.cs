namespace aoc.Year2024;

internal partial class Day05
{
	public record Page(List<int> Numbers)
	{
		public static Page Create(IEnumerable<int> numbers) => new(numbers.ToList());

		public int? FindIndex(int number)
		{
			var index = Numbers.IndexOf(number);
			return index == -1 ? null : index;
		}
		
		public int Middle() => Numbers[Numbers.Count / 2];
	}

	public record Rule(int Smaller, int Larger)
	{
		public static Rule Create(int smaller, int larger) => new(smaller, larger);

		public bool Satisfied(Page page)
		{
			var smallerIndex = page.FindIndex(Smaller);
			var largerIndex = page.FindIndex(Larger);
			if (smallerIndex is null || largerIndex is null)
				return true;
			return smallerIndex < largerIndex;
		}
	}

	public static (Rule[] rules, Page[] pages) ParseInput(string input)
	{
		var rulesParser =
			Numerics.IntegerInt32.ThenIgnore(Span.EqualTo("|"))
				.Then(Numerics.IntegerInt32)
				.Select(Rule.Create)
				.Lines();

		var pages =
			Numerics.IntegerInt32.ManyDelimitedBy(Span.EqualTo(","))
				.Select(Page.Create)
				.Lines();

		return rulesParser.Block().ThenBlock(pages).Parse(input);
	}

	internal partial class Part1
	{
		private readonly Example example = new(ExampleInput, 143);

		public int Solve((Rule[] rules, Page[] pages) input)
		{
			var correctPages 
				= input.pages.Where(page => input.rules.All(rule => rule.Satisfied(page)));
			return correctPages.Select(x => x.Middle()).Sum();
		}

		public (Rule[] rules, Page[] pages) Parse(string input) => ParseInput(input);
	}

	internal partial class Part2
	{
		private readonly Example example = new(ExampleInput, 123);

		public int Solve((Rule[] rules, Page[] pages) input)
		{
			var incorrectPages
				= input.pages.Where(page => ! input.rules.All(rule => rule.Satisfied(page)));

			return incorrectPages.Select(page =>
				Page.Create(page.Numbers.Order(new Comparer(input.rules))).Middle())
				.Sum();
		}

		private class Comparer(Rule[] rules) : IComparer<int>
		{
			public int Compare(int x, int y)
			{
				if (rules.Contains(Rule.Create(x, y)))
					return 1;
				if (rules.Contains(Rule.Create(y, x)))
					return -1;
				return 0;
			}
		}

		public (Rule[] rules, Page[] pages) Parse(string input) => ParseInput(input);
	}

	private const string ExampleInput =
		"""
		47|53
		97|13
		97|61
		97|47
		75|29
		61|13
		75|53
		29|13
		97|29
		53|29
		61|53
		97|53
		61|29
		47|13
		75|47
		97|75
		47|61
		75|61
		47|29
		75|13
		53|13

		75,47,61,53,29
		97,61,53,29,13
		75,29,13
		75,97,47,61,53
		61,13,29
		97,13,75,29,47
		""";
}