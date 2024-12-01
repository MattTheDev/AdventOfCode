using System.Reflection;

namespace AdventOfCode2024.Days;

/// <summary>
/// https://adventofcode.com/2024/day/1
/// Assumption: We're assuming that the input has an equal amount of inputs for both lists. We're also making the assumption that location IDs will safely parse to int.
/// </summary>
public class DayOne
{

    #region Part One 

    private static readonly List<int> ListOne = new();
    private static readonly List<int> ListTwo = new();
    private static readonly List<int> Differences = new();

    private static void Load()
    {
        var content = LoadInput();

        // Split based off newline
        foreach (var line in content.Trim().Split('\n'))
        {
            // Split based off spacing to get individual values
            var inputs = line.Split("   ");
            ListOne.Add(int.Parse(inputs[0]));
            ListTwo.Add(int.Parse(inputs[1]));
        }

        // Order our lists
        ListOne.Sort();
        ListTwo.Sort();
    }

    private static string LoadInput()
    {
        // Load embedded input
        var resourceName = "AdventOfCode2024.Input.DayOne.txt";
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd();

        return content;
    }

    private static void GetDifferences()
    {
        // Iterate and load differences
        for (var i = 0; i < ListOne.Count; i++)
        {
            Differences.Add(Math.Abs(ListOne[i] - ListTwo[i]));
        }
    }

    public static void SolvePartOne()
    {
        // Load data
        Load();
        // Get differences in location ids
        GetDifferences();
        // Output final results
        Console.WriteLine($"The answer for Day One Part One of Advent of Code is: {Differences.Sum()}");
    }

    #endregion

    #region Part Two

    private static readonly List<int> ListSimilarityScore = new();

    private static void GetSimilarityScore()
    {
        var listTwoCount = ListTwo.GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());

        // Only iterate over shared location ids.
        foreach (var locationId in ListOne.Intersect(ListTwo))
        {
            try
            {
                var similarity = listTwoCount[locationId];
                ListSimilarityScore.Add(similarity * locationId);
            }
            catch (Exception)
            {
                // Doesn't exist. Skip to next.
            }
        }
    }

    public static void SolvePartTwo()
    {
        // Get the similarity score
        GetSimilarityScore();

        // Output final results
        Console.WriteLine($"The answer for Day One Part Two of Advent of Code is: {ListSimilarityScore.Sum()}");
    }

    #endregion
}