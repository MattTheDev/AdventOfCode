using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode_2024.Days;

/// <summary>
/// https://adventofcode.com/2024/day/3
/// Assumption: We're assuming inputs are valid and parseable.
/// </summary>
public class DayThree
{
    public static void Solve()
    {
        // Get baseline input. This is used in part one.
        var input = LoadInput();
        // Get enabled (do() vs don't()) input. This is used in part two.
        var enabledInput = GetValidOperations(input);

        // Calculate results
        var partOne = Calculate(input);
        var partTwo = Calculate(enabledInput);

        // Output them
        Console.WriteLine($"The answer for Day Three Part One of Advent of Code is: {partOne}");
        Console.WriteLine($"The answer for Day Three Part Two of Advent of Code is: {partTwo}");
    }

    /// <summary>
    /// Iterate through split values. Return only items between do() and don't()
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static string GetValidOperations(string input)
    {
        var builder = new StringBuilder();
        var splits = input.Split("do()");
        foreach (var split in splits)
        {
            builder.Append(split.Split("don't()")[0]);
        }

        return builder.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static int Calculate(string input)
    {
        const string pattern = @"mul\((\d+),(\d+)\)";
        var regex = new Regex(pattern);

        var matches = regex.Matches(input);

        var total = 0;
        foreach (Match match in matches)
        {
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            total += (x * y);
        }

        return total;
    }

    /// <summary>
    /// Load input provided
    /// </summary>
    /// <returns></returns>
    private static string LoadInput()
    {
        // Load embedded input
        var resourceName = "AdventOfCode_2024.Input.DayThree.txt";
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}