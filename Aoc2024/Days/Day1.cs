namespace Aoc2024.Days;
public static class Day1
{
    public static async Task<(int, int)> ReadData()
    {
        var text = await File.ReadAllLinesAsync(@"C:\GitHub\aoc2024\inputs\day1.txt");

        var leftPart = new List<int>();
        var rightPart = new List<int>();

        foreach (var line in text)
        {
            var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            leftPart.Add(int.Parse(parts[0]));
            rightPart.Add(int.Parse(parts[1]));
        }

        leftPart.Sort();
        rightPart.Sort();

        // Part 1: Calculate the absolute differences
        var diff = leftPart.Select((t, i) => Math.Abs(t - rightPart[i])).Sum();

        // Part 2: Calculate the similarity score
        var rightPartFrequency = rightPart.GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.Count());

        var similarityScore = 0;

        foreach (var t in leftPart)
        {
            if (rightPartFrequency.TryGetValue(t, out var count))
            {
                similarityScore += count * t;
            }
        }

        return (diff, similarityScore);
    }
}
