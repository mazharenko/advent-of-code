using System.Collections;

namespace AoC.Tests;

public class Year2025Cases : IEnumerable
{
	public IEnumerator GetEnumerator()
	{
		yield return new PartInputCaseData(1, 1, "1048");
		yield return new PartInputCaseData(1, 2, "6498");
	}
}