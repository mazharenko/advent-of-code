# Advent of Code

Here are my solutions for the [Advent of Code event](https://adventofcode.com/).

## :two::zero::two::four:

I had been missing various features of C#. At the same time, I was wondering how well I could handle situations where I had previously relied heavily on specific features of F#. So, this year, I decided to solve the AoC puzzles in C#. And I would catch myself willing to have type aliases / tail recursion / structural equality / whatever, but generally speaking, it was a more comfortable experience, especially since I did not have to push myself to avoid using an imperative style.

Additionally, I was looking forward to improving the parsing process. I tried out [Superpower](https://github.com/datalust/superpower) and managed to achieve a concise syntax for some common patterns found in AoC inputs like lines and blocks. It worked fairly well, though dealing with its backtracking was tricky. I also succeeded to construct a parser from a template with use of custom interpolated string handlers, like the following: 
```csharp
Template.Matching<int, int>($"p={Numerics.IntegerInt32},{Numerics.IntegerInt32};") // TextParser<(int, int)>
```

<a href="src/aoc/Year2024/Day01.cs"><img src=".aoc_tiles/tiles/2024/01.png" width="161px"></a>
<a href="src/aoc/Year2024/Day02.cs"><img src=".aoc_tiles/tiles/2024/02.png" width="161px"></a>
<a href="src/aoc/Year2024/Day03.cs"><img src=".aoc_tiles/tiles/2024/03.png" width="161px"></a>
<a href="src/aoc/Year2024/Day04.cs"><img src=".aoc_tiles/tiles/2024/04.png" width="161px"></a>
<a href="src/aoc/Year2024/Day05.cs"><img src=".aoc_tiles/tiles/2024/05.png" width="161px"></a>
<a href="src/aoc/Year2024/Day06.cs"><img src=".aoc_tiles/tiles/2024/06.png" width="161px"></a>
<a href="src/aoc/Year2024/Day07.cs"><img src=".aoc_tiles/tiles/2024/07.png" width="161px"></a>
<a href="src/aoc/Year2024/Day08.cs"><img src=".aoc_tiles/tiles/2024/08.png" width="161px"></a>
<a href="src/aoc/Year2024/Day09.cs"><img src=".aoc_tiles/tiles/2024/09.png" width="161px"></a>
<a href="src/aoc/Year2024/Day10.cs"><img src=".aoc_tiles/tiles/2024/10.png" width="161px"></a>
<a href="src/aoc/Year2024/Day11.cs"><img src=".aoc_tiles/tiles/2024/11.png" width="161px"></a>
<a href="src/aoc/Year2024/Day12.cs"><img src=".aoc_tiles/tiles/2024/12.png" width="161px"></a>
<a href="src/aoc/Year2024/Day13.cs"><img src=".aoc_tiles/tiles/2024/13.png" width="161px"></a>
<a href="src/aoc/Year2024/Day14.cs"><img src=".aoc_tiles/tiles/2024/14.png" width="161px"></a>
<a href="src/aoc/Year2024/Day15.cs"><img src=".aoc_tiles/tiles/2024/15.png" width="161px"></a>
<a href="src/aoc/Year2024/Day16.cs"><img src=".aoc_tiles/tiles/2024/16.png" width="161px"></a>
<a href="src/aoc/Year2024/Day17.cs"><img src=".aoc_tiles/tiles/2024/17.png" width="161px"></a>
<a href="src/aoc/Year2024/Day18.cs"><img src=".aoc_tiles/tiles/2024/18.png" width="161px"></a>
<a href="src/aoc/Year2024/Day19.cs"><img src=".aoc_tiles/tiles/2024/19.png" width="161px"></a>
<a href="src/aoc/Year2024/Day20.cs"><img src=".aoc_tiles/tiles/2024/20.png" width="161px"></a>
<a href="src/aoc/Year2024/Day21.cs"><img src=".aoc_tiles/tiles/2024/21.png" width="161px"></a>
<a href="src/aoc/Year2024/Day22.cs"><img src=".aoc_tiles/tiles/2024/22.png" width="161px"></a>
<a href="src/aoc/Year2024/Day23.cs"><img src=".aoc_tiles/tiles/2024/23.png" width="161px"></a>
<a href="src/aoc/Year2024/Day24.cs"><img src=".aoc_tiles/tiles/2024/24.png" width="161px"></a>
<a href="src/aoc/Year2024/Day25.cs"><img src=".aoc_tiles/tiles/2024/25.png" width="161px"></a>

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

<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day01.fs"><img src=".aoc_tiles/tiles/2023/01.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day02.fs"><img src=".aoc_tiles/tiles/2023/02.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day03.fs"><img src=".aoc_tiles/tiles/2023/03.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day04.fs"><img src=".aoc_tiles/tiles/2023/04.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day05.fs"><img src=".aoc_tiles/tiles/2023/05.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day06.fs"><img src=".aoc_tiles/tiles/2023/06.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day07.fs"><img src=".aoc_tiles/tiles/2023/07.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day08.fs"><img src=".aoc_tiles/tiles/2023/08.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day09.fs"><img src=".aoc_tiles/tiles/2023/09.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day10.fs"><img src=".aoc_tiles/tiles/2023/10.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day11.fs"><img src=".aoc_tiles/tiles/2023/11.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day12.fs"><img src=".aoc_tiles/tiles/2023/12.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day13.fs"><img src=".aoc_tiles/tiles/2023/13.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day14.fs"><img src=".aoc_tiles/tiles/2023/14.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day15.fs"><img src=".aoc_tiles/tiles/2023/15.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day16.fs"><img src=".aoc_tiles/tiles/2023/16.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day17.fs"><img src=".aoc_tiles/tiles/2023/17.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day18.fs"><img src=".aoc_tiles/tiles/2023/18.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day19.fs"><img src=".aoc_tiles/tiles/2023/19.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day20.fs"><img src=".aoc_tiles/tiles/2023/20.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day21.fs"><img src=".aoc_tiles/tiles/2023/21.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day22.fs"><img src=".aoc_tiles/tiles/2023/22.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day23.fs"><img src=".aoc_tiles/tiles/2023/23.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day24.fs"><img src=".aoc_tiles/tiles/2023/24.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc-2023/blob/main/src/impl/day25.fs"><img src=".aoc_tiles/tiles/2023/25.png" width="161px"></a>

## :two::zero::two::two:

[AoC-2022](https://github.com/mazharenko/AoC-2022)

More F# and more visualization in a notebook. Try [FParsec](https://github.com/stephan-tolksdorf/fparsec) to parse input. Try lenses 😱.

<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day01"><img src=".aoc_tiles/tiles/2022/01.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day02"><img src=".aoc_tiles/tiles/2022/02.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day03"><img src=".aoc_tiles/tiles/2022/03.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day04"><img src=".aoc_tiles/tiles/2022/04.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day05"><img src=".aoc_tiles/tiles/2022/05.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day06"><img src=".aoc_tiles/tiles/2022/06.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day07"><img src=".aoc_tiles/tiles/2022/07.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day08"><img src=".aoc_tiles/tiles/2022/08.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day09"><img src=".aoc_tiles/tiles/2022/09.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day10"><img src=".aoc_tiles/tiles/2022/10.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day11"><img src=".aoc_tiles/tiles/2022/11.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day12"><img src=".aoc_tiles/tiles/2022/12.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day13"><img src=".aoc_tiles/tiles/2022/13.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day14"><img src=".aoc_tiles/tiles/2022/14.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day15"><img src=".aoc_tiles/tiles/2022/15.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day16"><img src=".aoc_tiles/tiles/2022/16.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day17"><img src=".aoc_tiles/tiles/2022/17.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day18"><img src=".aoc_tiles/tiles/2022/18.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day19"><img src=".aoc_tiles/tiles/2022/19.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day20"><img src=".aoc_tiles/tiles/2022/20.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day21"><img src=".aoc_tiles/tiles/2022/21.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day22"><img src=".aoc_tiles/tiles/2022/22.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day23"><img src=".aoc_tiles/tiles/2022/23.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day24"><img src=".aoc_tiles/tiles/2022/24.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2022/tree/main/notebooks/day25"><img src=".aoc_tiles/tiles/2022/25.png" width="161px"></a>


## :two::zero::two::one:

[AoC-2021](https://github.com/mazharenko/AoC-2021)

Continue playing with F#. Discovered wonderful .NET Interactive notebooks. Invested into the presentation of the results: mermaid, plotly, canvas.

<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day01"><img src=".aoc_tiles/tiles/2021/01.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day02"><img src=".aoc_tiles/tiles/2021/02.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day03"><img src=".aoc_tiles/tiles/2021/03.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day04"><img src=".aoc_tiles/tiles/2021/04.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day05"><img src=".aoc_tiles/tiles/2021/05.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day06"><img src=".aoc_tiles/tiles/2021/06.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day07"><img src=".aoc_tiles/tiles/2021/07.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day08"><img src=".aoc_tiles/tiles/2021/08.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day09"><img src=".aoc_tiles/tiles/2021/09.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day10"><img src=".aoc_tiles/tiles/2021/10.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day11"><img src=".aoc_tiles/tiles/2021/11.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day12"><img src=".aoc_tiles/tiles/2021/12.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day13"><img src=".aoc_tiles/tiles/2021/13.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day14"><img src=".aoc_tiles/tiles/2021/14.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day15"><img src=".aoc_tiles/tiles/2021/15.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day16"><img src=".aoc_tiles/tiles/2021/16.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day17"><img src=".aoc_tiles/tiles/2021/17.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day18"><img src=".aoc_tiles/tiles/2021/18.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day19"><img src=".aoc_tiles/tiles/2021/19.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day20"><img src=".aoc_tiles/tiles/2021/20.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day21"><img src=".aoc_tiles/tiles/2021/21.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day22"><img src=".aoc_tiles/tiles/2021/22.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day23"><img src=".aoc_tiles/tiles/2021/23.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day24"><img src=".aoc_tiles/tiles/2021/24.png" width="161px"></a>
<a href="https://github.com/mazharenko/AoC-2021/tree/main/notebooks/day25"><img src=".aoc_tiles/tiles/2021/25.png" width="161px"></a>

## :two::zero::two::zero:

[aoc2020](https://github.com/mazharenko/aoc2020)

First time taking part in the event. As a C# developer who was reading a lot about F#, wanted to give it a try on "real" tasks. It showed to be quite suitable for many puzzles thanks to immutability and structural comparison out of the box.


<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle01.fs"><img src=".aoc_tiles/tiles/2020/01.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle02.fs"><img src=".aoc_tiles/tiles/2020/02.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle03.fs"><img src=".aoc_tiles/tiles/2020/03.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle04_1.fs"><img src=".aoc_tiles/tiles/2020/04.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle05.fs"><img src=".aoc_tiles/tiles/2020/05.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle06.fs"><img src=".aoc_tiles/tiles/2020/06.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle07.fs"><img src=".aoc_tiles/tiles/2020/07.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle08.fs"><img src=".aoc_tiles/tiles/2020/08.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle09.fs"><img src=".aoc_tiles/tiles/2020/09.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle10.fs"><img src=".aoc_tiles/tiles/2020/10.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle11.fs"><img src=".aoc_tiles/tiles/2020/11.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle12.fs"><img src=".aoc_tiles/tiles/2020/12.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle13.fs"><img src=".aoc_tiles/tiles/2020/13.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle14.fs"><img src=".aoc_tiles/tiles/2020/14.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle15.fs"><img src=".aoc_tiles/tiles/2020/15.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle16.fs"><img src=".aoc_tiles/tiles/2020/16.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle17.fs"><img src=".aoc_tiles/tiles/2020/17.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle18.fs"><img src=".aoc_tiles/tiles/2020/18.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle19.fs"><img src=".aoc_tiles/tiles/2020/19.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle20.fs"><img src=".aoc_tiles/tiles/2020/20.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle21.fs"><img src=".aoc_tiles/tiles/2020/21.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle22.fs"><img src=".aoc_tiles/tiles/2020/22.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle23.fs"><img src=".aoc_tiles/tiles/2020/23.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle24.fs"><img src=".aoc_tiles/tiles/2020/24.png" width="161px"></a>
<a href="https://github.com/mazharenko/aoc2020/blob/master/AoC2020_1/puzzle25.fs"><img src=".aoc_tiles/tiles/2020/25.png" width="161px"></a>