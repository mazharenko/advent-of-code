namespace Common.Tests.Parsing;

public class TemplateTests
{
	[Test]
	public void Should_Parse_Tuple1()
	{
		var parser = Template.Matching<int, int>(
			$"p={Numerics.IntegerInt32},{Numerics.IntegerInt32};"
		);

		var result = parser.AtEnd().TryParse("p=3434,8787;");
		result.HasValue.Should().BeTrue();
		result.Value.Should().Be((3434, 8787));
	}

	[Test]
	public void Should_Parse_Tuple2()
	{
		var parser = Template.Matching<(int, int)>(
			$"p={Numerics.IntegerInt32},{Numerics.IntegerInt32};"
		);

		var result = parser.AtEnd().TryParse("p=3434,8787;");
		result.HasValue.Should().BeTrue();
		result.Value.Should().Be((3434, 8787));
	}

	[Test]
	public void Should_Parse_SingleValue()
	{
		var parser = Template.Matching<int>(
			$"p={Numerics.IntegerInt32};"
		);

		var result = parser.AtEnd().TryParse("p=3434;");
		result.HasValue.Should().BeTrue();
		result.Value.Should().Be(3434);
	}

	[Test]
	public void Should_Return_Remainder()
	{
		var parser = Template.Matching<int>(
			$"p={Numerics.IntegerInt32};"
		);

		var result = parser.TryParse("p=3434;v=9898");
		result.HasValue.Should().BeTrue();
		result.Value.Should().Be(3434);
		result.Remainder.ToStringValue().Should().Be("v=9898");
	}
}