using System.CommandLine;
using mazharenko.AoCAgent.GeneratedRunner;
using Spectre.Console;



var yearArgument = new Argument<int>("year");
var dayArgument = new Argument<int>("day");
var partArgument = new Argument<int>("part");
var inputOption = new Option<string>("--input-file") { Required = true };
var runCommand = new Command("run")
{
	yearArgument, dayArgument, inputOption, partArgument
};
var examplesCommand = new Command("examples")
{
	yearArgument, dayArgument, partArgument
};

var rootCommand = new RootCommand { runCommand, examplesCommand };

rootCommand.SetAction(async _ =>
{
	var year = new mazharenko.AoCAgent.GeneratedAgent.Year2025();
	await new mazharenko.AoCAgent.Runner().Run(year);
});

examplesCommand.SetAction(result => 
{
	var yearNum = result.GetRequiredValue(yearArgument);
	var dayNum = result.GetRequiredValue(dayArgument);
	var partNum = result.GetRequiredValue(partArgument);
	var year = new YearCollection().FirstOrDefault(y => y.Year == yearNum);
	if (year is null)
		throw new ArgumentException($"Year {yearNum} not defined");
	var part = year.Parts.FirstOrDefault(p => p.Day.Num == dayNum && p.PartNum.Num == partNum);
	if (part is null)
		throw new ArgumentException($"Part {yearNum}/{dayNum}/{partNum} not found");
	var examples = part.Part.GetExamples().ToList();

	AnsiConsole.WriteLine($"Running examples for {yearNum}/{dayNum}/{partNum}");
	if (examples.Count == 0)
		AnsiConsole.WriteLine("No examples");
	else
	{
		var table = new Table();
		table.AddColumn("Day");
		table.AddColumn("Part");
		table.AddColumn("Example");
		table.AddColumn("Expected");
		table.AddColumn("Actual");
		table.SimpleBorder();
		foreach (var namedExample in examples)
		{
			try
			{
				var actual = namedExample.Example.RunFormat(out var actualFormatted);
				table.AddRow(dayNum.ToString(), part.PartNum.ToString(),
					namedExample.Name,
					namedExample.Example.ExpectationFormatted,
					Equals(actual, namedExample.Example.Expectation)
						? "[green]correct[/]"
						: $"[red]{Markup.Escape(actualFormatted)}[/]");
			}
			catch (NotImplementedException)
			{
				table.AddRow(dayNum.ToString(), part.PartNum.ToString(),
					namedExample.Name,
					namedExample.Example.ExpectationFormatted,
					"[grey]not implemented[/]");
			}
			catch (Exception e)
			{
				table.AddRow(dayNum.ToString(), part.PartNum.ToString(),
					namedExample.Name,
					namedExample.Example.ExpectationFormatted,
					$"[red]{Markup.Escape(e.Message)}[/]");
			}
		}

		AnsiConsole.Write(table);
	}

});

runCommand.SetAction(result =>
{
	var yearNum = result.GetRequiredValue(yearArgument);
	var dayNum = result.GetRequiredValue(dayArgument);
	var partNum = result.GetRequiredValue(partArgument);
	var input = File.ReadAllText(result.GetRequiredValue(inputOption));
	var year = new YearCollection().FirstOrDefault(y => y.Year == yearNum);
	if (year is null)
		throw new ArgumentException($"Year {yearNum} not defined");
	var part = year.Parts.FirstOrDefault(p => p.Day.Num == dayNum && p.PartNum.Num == partNum);
	if (part is null)
		throw new ArgumentException($"Part {yearNum}/{dayNum}/{partNum} not found");
	
	AnsiConsole.WriteLine($"Calculating answer for {yearNum}/{dayNum}/{partNum}");
	var answer = part.Part.SolveString(input);
	AnsiConsole.WriteLine($"Answer: {answer}");
});


try
{
	await rootCommand.Parse(args).InvokeAsync();
	return 0;
}
catch (Exception e)
{
	AnsiConsole.WriteException(e);
	return 134;
}
