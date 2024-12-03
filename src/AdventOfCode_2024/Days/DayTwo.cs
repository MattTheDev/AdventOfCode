using System.Reflection;

namespace AdventOfCode_2024.Days;

/// <summary>
/// https://adventofcode.com/2024/day/2
/// Assumption: We're assuming inputs are valid and parseable.
/// </summary>
public class DayTwo
{
    private static string[] LoadInput()
    {
        // Load embedded input
        var resourceName = "AdventOfCode_2024.Input.DayTwo.txt";
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd().Split('\n');
    }

    public static void SolveBothParts()
    {
        var inputs = LoadInput();

        var valid = inputs.Select(line => line.Split(' ').Select(int.Parse).ToList()).Where(IsValid).ToList();
        Console.WriteLine($"The answer for Day Two Part One of Advent of Code is: {valid.Count}");

        valid = inputs.Select(line => line.Split(' ').Select(int.Parse).ToList()).Where(IsValidWithTolerance).ToList();
        Console.WriteLine($"The answer for Day Two Part Two of Advent of Code is: {valid.Count}");
    }

    // If we can remove a single item from the list, would it pass?
    private static bool IsValidWithTolerance(List<int> ints)
    {
        // Get all potential combinations
        var potentialCombinations = PotentialCombinations(ints);

        // Run all combinations through our validity check.
        // If ANY pass, short circuit and return true.
        return potentialCombinations.Any(IsValid);
    }

    // Check validity based off the following rules:
    // - The levels are either all increasing or all decreasing.
    // - Any two adjacent levels differ by at least one and at most three.
    private static bool IsValid(List<int> ints)
    {
        var direction = string.Empty;
        // Iterate over ints in list
        for (var x = 0; x < ints.Count - 1; x++)
        {
            // Get the diff/abs diff for comparison.
            var rawDiff = ints[x] - ints[x + 1];
            var absDiff = Math.Abs(rawDiff);

            // If we haven't made a change, or it's been greater than 3, fail. 
            if (absDiff is 0 or > 3)
            {
                return false;
            }

            // If this is the initial run, set asc vs. desc
            if (x == 0)
            {
                direction = rawDiff < 0 ? "-" : "+";
            }
            else
            {
                // Get the current iteration asc. vs. desc.
                var newDirection = rawDiff < 0 ? "-" : "+";

                // If we're not consistent in asc. vs. desc, fail
                if (direction != newDirection)
                {
                    return false;
                }
            }
        }

        return true;
    }

    // Get the potential list of combinations from removing a single element from a list of input.
    private static List<List<int>> PotentialCombinations(List<int> ints)
    {
        var combinations = new List<List<int>>();
        for (var i = 0; i < ints.Count; i++)
        {
            var combination = new List<int>(ints);
            combination.RemoveAt(i);
            combinations.Add(combination);
        }

        return combinations;
    }
}