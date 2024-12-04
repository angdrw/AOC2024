namespace Day2;

class Program
{
    static void Main(string[] args)
    {
        // Part1();
        Part2();
    }

    static void Part1()
    {
        string fileName = "puzzleInput.txt";

        var reports = File.ReadAllLines(fileName);

        int numSafeReports = 0;

        foreach (var report in reports)
        {
            List<int> levels = report.Split(" ", StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

            bool isReportSafe = true;
            bool isIncreasing = false;

            for (int i = 1; i < levels.Count(); i++)
            {
                int diff = levels[i] - levels[i - 1];

                if (i == 1)
                    isIncreasing = diff > 0;
                else
                    if ((isIncreasing && diff <= 0) || (!isIncreasing && diff > 0))
                    {
                        isReportSafe = false;
                        break;
                    }

                if (diff == 0 || Math.Abs(diff) > 3)
                {
                    isReportSafe = false;
                    break;
                }
            }

            if (isReportSafe)
                numSafeReports++;
        }

        Console.WriteLine($"numSafeReports: {numSafeReports}");
    }

    static void Part2()
    {
        string fileName = "puzzleInput.txt";

        var reports = File.ReadAllLines(fileName);

        int numSafeReports = 0;

        foreach (var report in reports)
        {
            List<int> levels = report.Split(" ", StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

            if (CheckReportLevels(levels))
                numSafeReports++;
        }

        Console.WriteLine($"numSafeReports: {numSafeReports}");
    }

    private static (bool, bool) IsLevelSafe(int i, int num1, int num2, bool isIncreasing)
    {
            int diff = num1 - num2;

            if (i == 1)
                isIncreasing = diff > 0;
            else
                if ((isIncreasing && diff <= 0) || (!isIncreasing && diff >= 0))
                {
                    return (false, isIncreasing);
                }

            if (diff == 0 || Math.Abs(diff) > 3)
                return (false, isIncreasing);

            return (true, isIncreasing);
    }

    private static bool CheckReportLevels(List<int> levels, bool hasFailed = false)
    {
        bool isReportSafe = true;
        bool isIncreasing = false;

        for (int i = 1; i < levels.Count(); i++)
        {
            (isReportSafe, isIncreasing) = IsLevelSafe(i, levels[i], levels[i-1], isIncreasing);

            if (!isReportSafe && !hasFailed)
            {
                for (int y = 0; y < levels.Count(); y++)
                {
                    List<int> newLevels = new(levels);
                    newLevels.RemoveAt(y);
                    isReportSafe = CheckReportLevels(newLevels, true);

                    if (isReportSafe)
                        break;
                }
            }
            else if (!isReportSafe && hasFailed)
                break;

            if (!isReportSafe)
                break;
        }

        return isReportSafe;
    }
}
