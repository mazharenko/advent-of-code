using mazharenko.AoCAgent.Generator;

namespace AoC.Tests;

[TestFixture]
[GenerateInputTests(2024, nameof(GetCases))]
internal partial class InputTests
{
	private static IEnumerable<PartInputCaseData> GetCases()
	{
		yield return new PartInputCaseData(1, 1, "1889772");
		yield return new PartInputCaseData(1, 2, "23228917");
		yield return new PartInputCaseData(2, 1, "359");
		yield return new PartInputCaseData(2, 2, "418");
		yield return new PartInputCaseData(3, 1, "173529487");
		yield return new PartInputCaseData(3, 2, "99532691");
	}
}