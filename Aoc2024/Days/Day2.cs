namespace Aoc2024.Days;

public static class Day2
{
    public static async Task<int> GetResult()
    {
        var text = await File.ReadAllLinesAsync(@"C:\GitHub\aoc2024\inputs\day2.txt");

        var rows = text
            .Select(line => line.Split(' ').Select(int.Parse).ToArray())
            .ToList();

        return rows.AsParallel().Sum(IsSafe);
    }

    private static int IsSafe(int[] report)
    {
        if (report.Length < 2) return 1;

        if (IsValidSequence(report))
            return 1;

        return report.Where((t, i) => IsValidSequence(RemoveElement(report, i))).Any() ? 1 : 0;
    }

    private static bool IsValidSequence(int[] report)
    {
        var isIncreasing = true;
        var isDecreasing = true;

        for (var i = 1; i < report.Length; i++)
        {
            var diff = Math.Abs(report[i] - report[i - 1]);

            if (diff is < 1 or > 3) return false;

            if (report[i] > report[i - 1]) isDecreasing = false;
            else if (report[i] < report[i - 1]) isIncreasing = false;

            if (!isIncreasing && !isDecreasing) return false;
        }

        return isIncreasing || isDecreasing;
    }

    private static int[] RemoveElement(int[] arr, int index)
    {
        var result = new int[arr.Length - 1];

        new Span<int>(arr, 0, index).CopyTo(result);
        new Span<int>(arr, index + 1, arr.Length - index - 1).CopyTo(result.AsSpan().Slice(index));

        return result;
    }
}