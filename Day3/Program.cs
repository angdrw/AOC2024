using System.Text.RegularExpressions;

namespace Day3;

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

        int sum = 0;

        foreach (var line in lines)
            sum += GetMulSums(line);

        Console.WriteLine($"sum = {sum}");
    }

    private static int GetMulSums(string line)
    {
        int sum = 0;
        var matches = Regex.Matches(line, "mul\\([0-9]*,[0-9]*\\)");

        foreach (Match match in matches)
        {
            var parts = match.Value.Split(',');

            var firstNum = int.Parse(new string(parts[0].Where(char.IsDigit).ToArray()));
            var secondNum = int.Parse(new string(parts[1].Where(char.IsDigit).ToArray()));

            sum += firstNum * secondNum;
        }

        return sum;
    }

    private static void Part2()
    {
        string filename = "puzzleInput.txt";

        var line = File.ReadAllText(filename);

        int sum = 0;

        var dontParts = line.Split("don't()");

        for (int i = 0; i < dontParts.Count(); i++)
            if (i == 0)
                sum += GetMulSums(dontParts[0]);
            else
            {
                var doParts = dontParts[i].Split("do()");

                //the first part of doParts we can forget about because we had a leading 'dont'
                //every other part, if any, we need to check and add to sum
                if (doParts.Count() >= 2)
                    for (int y = 1; y < doParts.Count(); y++)
                        sum += GetMulSums(doParts[y]);
            }

        Console.WriteLine($"sum = {sum}");
    }
}
