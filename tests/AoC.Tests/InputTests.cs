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
		yield return new PartInputCaseData(4, 1, "2646");
		yield return new PartInputCaseData(4, 2, "2000");
		yield return new PartInputCaseData(5, 1, "5639");
		yield return new PartInputCaseData(5, 2, "5273");
		yield return new PartInputCaseData(6, 1, "4722");
		yield return new PartInputCaseData(6, 2, "1602");
		yield return new PartInputCaseData(7, 1, "21572148763543");
		yield return new PartInputCaseData(7, 2, "581941094529163");
	}
}