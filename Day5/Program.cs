namespace Day5;

class Program
{
    static void Main(string[] args)
    {
        //Part1();
        Part2();
    }

    private static void Part1()
    {
        string filename = "puzzleInput.txt";

        var lines = File.ReadAllLines(filename);

        Dictionary<int, List<int>> sortingRules = [];
        var sortingRuleLines = lines.Where(line => line.Contains('|')).ToList();

        var updateLines = lines.Where(line => line.Contains(',')).ToList();

        foreach (var sortingRuleLine in sortingRuleLines)
        {
            var parts = sortingRuleLine.Split('|');

            var num1 = int.Parse(parts[0]);
            var num2 = int.Parse(parts[1]);

            if (sortingRules.Keys.Contains(num1))
            {
                if (!sortingRules[num1].Contains(num2))
                    sortingRules[num1].Add(num2);
            }
            else
                sortingRules.Add(num1, [ num2 ]);
        }

        int sum = 0;

        foreach (var updateLine in updateLines)
        {
            var parts = updateLine.Split(',');

            List<int> update = parts.Select(int.Parse).ToList();

            bool correctOrder = true;

            for (int i = 0; i < update.Count(); i++)
            {
                if (!sortingRules.Keys.Contains(update[i]))
                    continue;

                var rules = sortingRules[update[i]];

                for (int y = i > 0 ? 0 : i + 1; y < update.Count(); y++)
                {
                    if (y == i)
                        continue;

                    if (rules.Contains(update[y]) && y < i)
                        correctOrder = false;
                }
            }

            if (correctOrder)
                sum += update[update.Count() / 2];
        }

        Console.WriteLine($"sum = {sum}");
    }

    private static void Part2()
    {
        string filename = "puzzleInput.txt";

        var lines = File.ReadAllLines(filename);

        Dictionary<int, List<int>> sortingRules = [];
        var sortingRuleLines = lines.Where(line => line.Contains('|')).ToList();

        var updateLines = lines.Where(line => line.Contains(',')).ToList();

        foreach (var sortingRuleLine in sortingRuleLines)
        {
            var parts = sortingRuleLine.Split('|');

            var num1 = int.Parse(parts[0]);
            var num2 = int.Parse(parts[1]);

            if (sortingRules.Keys.Contains(num1))
            {
                if (!sortingRules[num1].Contains(num2))
                    sortingRules[num1].Add(num2);
            }
            else
                sortingRules.Add(num1, [ num2 ]);
        }

        List<List<int>> incorrectUpdates = [];

        foreach (var updateLine in updateLines)
        {
            var parts = updateLine.Split(',');

            List<int> update = parts.Select(int.Parse).ToList();

            bool correctOrder = true;

            for (int i = 0; i < update.Count(); i++)
            {
                if (!sortingRules.Keys.Contains(update[i]))
                    continue;

                var rules = sortingRules[update[i]];

                for (int y = i > 0 ? 0 : i + 1; y < update.Count(); y++)
                {
                    if (y == i)
                        continue;

                    if (rules.Contains(update[y]) && y < i)
                        correctOrder = false;
                }
            }

            if (!correctOrder)
                incorrectUpdates.Add(update);
        }

        int sum = 0;

        foreach (var incorrectUpdate in incorrectUpdates)
        {
            incorrectUpdate.Sort((n1, n2) => {
                if (n1 == n2)
                    return 0;
                else if (sortingRules.Keys.Contains(n1) && sortingRules.Keys.Contains(n2))
                    return sortingRules[n1].Contains(n2) ? -1 : 1;
                else if (sortingRules.Keys.Contains(n1))
                    return sortingRules[n1].Contains(n2) ? -1 : 0;
                else if (sortingRules.Keys.Contains(n2))
                    return sortingRules[n2].Contains(n1) ? 1 : 0;
                else
                    return 0;
            });

            sum += incorrectUpdate[incorrectUpdate.Count() / 2];
        }

        Console.WriteLine($"sum = {sum}");
    }
}
