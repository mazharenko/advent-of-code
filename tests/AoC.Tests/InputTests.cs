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
    }
}