using mazharenko.AoCAgent.Generator;

namespace AoC.Tests;

[TestFixture]
[GenerateInputTests(2024, typeof(Year2024Cases))]
[GenerateInputTests(2025, typeof(Year2025Cases))]
internal partial class InputTests;