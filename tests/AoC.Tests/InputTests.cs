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
		yield return new PartInputCaseData(8, 1, "285");
		yield return new PartInputCaseData(8, 2, "944");
		yield return new PartInputCaseData(9, 1, "6279058075753");
		yield return new PartInputCaseData(9, 2, "6301361958738");
		yield return new PartInputCaseData(10, 1, "517");
		yield return new PartInputCaseData(10, 2, "1116");
		yield return new PartInputCaseData(11, 1, "235850");
		yield return new PartInputCaseData(11, 2, "279903140844645");
		yield return new PartInputCaseData(12, 1, "1486324");
		yield return new PartInputCaseData(12, 2, "898684");
		yield return new PartInputCaseData(13, 1, "35255");
		yield return new PartInputCaseData(13, 2, "87582154060429");
		yield return new PartInputCaseData(14, 1, "236628054");
		yield return new PartInputCaseData(14, 2, "7584");
		yield return new PartInputCaseData(15, 1, "1516281");
		yield return new PartInputCaseData(15, 2, "1527969");
		yield return new PartInputCaseData(16, 1, "65436");
		yield return new PartInputCaseData(16, 2, "489");
		yield return new PartInputCaseData(17, 1, "7,1,3,7,5,1,0,3,4");
		yield return new PartInputCaseData(17, 2, "190384113204239");
		yield return new PartInputCaseData(18, 1, "284");
		yield return new PartInputCaseData(18, 2, "51,50");
		yield return new PartInputCaseData(19, 1, "338");
		yield return new PartInputCaseData(19, 2, "841533074412361");
	}
}