# Advent of Code

Here are my solutions for the [Advent of Code event](https://adventofcode.com/).

## :two::zero::two::four:

I had been missing various features of C#. At the same time, I found myself wondering how well I could handle situations where I had previously relied heavily on specific features of F#. So, this year, I decided to solve the AoC puzzles in C#.

Additionally, I’m looking forward to improving the parsing process. I plan to try out [Superpower](https://github.com/datalust/superpower) and invest in some extensions to achieve a concise syntax for some common patterns found in AoC inputs.

## :two::zero::two::three:

[aoc-2023](https://github.com/mazharenko/aoc-2023)

No longer happy with notebooks. Decided to sacrifice visualization at all and focus on a different thing: on optimizing the routine when solving puzzles, namely:

1. Go to the site and download the input.
2. Feed the input to the program.
3. Copy the result to the clipboard.
4. Paste it into the browser.
5. Doh, wrong.
6. Fix, compile, run, copy, paste.
7. Doh, "You gave an answer too recently."
8. Wait.
9. Repeat.

This is how I came up with the idea of [aoc-agent](https://github.com/mazharenko/aoc-agent) — a C# source generator that turns your library with actual algorithms into a self-validating, self-running console application that calculates and submits answers automatically.

Once again, F#. Found [Farkle](https://teo-tsirpanis.github.io/Farkle/) to be much better, still cumbersome and mind-numbing sometimes.

## :two::zero::two::two:

[AoC-2022](https://github.com/mazharenko/AoC-2022)

More F# and more visualization in a notebook. Try [FParsec](https://github.com/stephan-tolksdorf/fparsec) to parse input. Try lenses 😱.

## :two::zero::two::one:

[AoC-2021](https://github.com/mazharenko/AoC-2021)

Continue playing with F#. Discovered wonderful .NET Interactive notebooks. Invested into the presentation of the results: mermaid, plotly, canvas.

## :two::zero::two::zero:

[aoc2020](https://github.com/mazharenko/aoc2020)

First time taking part in the event. As a C# developer who was reading a lot about F#, wanted to give it a try on "real" tasks. It showed to be quite suitable for many puzzles thanks to immutability and structural comparison out of the box.