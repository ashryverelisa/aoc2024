using System.Text.RegularExpressions;

namespace Aoc2024.Days;

public static partial class Day3
{
    public static async Task<int> GetResult()
    {
        var corruptedMemory = await File.ReadAllTextAsync(@"C:\GitHub\aoc2024\inputs\day3.txt");

        var mulRegex = MulRegex();
        var toggleRegex = ToggleRegex();

        var totalSum = 0;
        var isEnabled = true;

        var toggleMatches = toggleRegex.Matches(corruptedMemory);
        var mulMatches = mulRegex.Matches(corruptedMemory);

        var allMatches = new List<(int Index, string Type, Match Match)>();

        foreach (Match match in toggleMatches)
        {
            allMatches.Add((match.Index, "toggle", match));
        }

        foreach (Match match in mulMatches)
        {
            allMatches.Add((match.Index, "mul", match));
        }

        allMatches.Sort((a, b) => a.Index.CompareTo(b.Index));

        foreach (var entry in allMatches)
        {
            switch (entry.Type)
            {
                case "toggle":
                {
                    var toggleValue = entry.Match.Groups[1].Value;
                    isEnabled = toggleValue switch
                    {
                        "do" => true,
                        "don't" => false,
                        _ => isEnabled
                    };
                    break;
                }
                case "mul" when isEnabled:
                {
                    var x = int.Parse(entry.Match.Groups[1].Value);
                    var y = int.Parse(entry.Match.Groups[2].Value);

                    totalSum += x * y;
                    break;
                }
            }
        }

        return totalSum;
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"(do|don't)\(\)")]
    private static partial Regex ToggleRegex();
}