using System.Reflection;

namespace AdventOfCode2024.Days;

public class DayTwo
{
    private static string[] _inputs;

    private static void LoadInput()
    {
        // Load embedded input
        var resourceName = "AdventOfCode2024.Input.DayTwo.txt";
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        _inputs = reader.ReadToEnd().Split('\n');
    }

    public static void SolvePartOne()
    {
        LoadInput();

        var valid = _inputs.Select(line => line.Split(' ').Select(int.Parse).ToList()).Where(IsValid).ToList();
        Console.WriteLine($"The answer for Day Two Part One of Advent of Code is: {valid.Count}");
    }

    private static bool IsValid(List<int> ints)
    {
        var direction = "+";
        for (var x = 0; x < ints.Count - 1; x++)
        {
            var rawDiff = ints[x] - ints[x + 1];
            var absDiff = Math.Abs(rawDiff);
            if (absDiff is <= 0 or > 3)
            {
                return false;
            }

            if (x == 0)
            {
                direction = rawDiff < 0 ? "-" : "+";
            }
            else
            {
                var newDirection = rawDiff < 0 ? "-" : "+";
                if (direction != newDirection)
                {
                    return false;
                }
            }
        }

        return true;
    }
}