using System.Collections;

namespace AoC.Tests;

public class Year2025Cases : IEnumerable
{
	public IEnumerator GetEnumerator()
	{
		yield return new PartInputCaseData(1, 1, "1048");
		yield return new PartInputCaseData(1, 2, "6498");
		yield return new PartInputCaseData(2, 1, "38437576669");
		yield return new PartInputCaseData(2, 2, "49046150754");
		yield return new PartInputCaseData(3, 1, "17263");
		yield return new PartInputCaseData(3, 2, "170731717900423");
		yield return new PartInputCaseData(4, 1, "1502");
		yield return new PartInputCaseData(4, 2, "9083");
		yield return new PartInputCaseData(5, 1, "726");
		yield return new PartInputCaseData(5, 2, "354226555270043");
		yield return new PartInputCaseData(6, 1, "5782351442566");
		yield return new PartInputCaseData(6, 2, "10194584711842");
	}
}