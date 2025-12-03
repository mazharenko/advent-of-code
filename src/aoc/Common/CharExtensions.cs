namespace aoc.Common;

public static class CharExtensions
{
	extension(char c)
	{
		public int ToInteger() => c - '0';
	}
}