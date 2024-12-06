namespace Day6;

class Program
{
    static void Main(string[] args)
    {
        //Part1();
        Part2();
    }

    private static void Part1()
    {
        var filename = "puzzleInput.txt";

        var lines = File.ReadAllLines(filename).ToList();

        var startingPosLine = lines.First(l => l.Contains('^'));
        var yPos = lines.IndexOf(startingPosLine);
        var xPos = startingPosLine.IndexOf("^");

        var traverseResult = Traverse(lines.ToArray(), yPos, xPos).Select(r => (r.Item1, r.Item2)).ToList();

        Console.WriteLine($"result = {traverseResult.Distinct().Count()}");
    }

    private static HashSet<(int, int, string)> Traverse(string[] lines, int yPos, int xPos)
    {
        HashSet<(int, int, string)> result = [];

        var prevYPos = yPos;
        var prevXPos = xPos;

        while (true)
        {
            var added = result.Add((yPos, xPos, $"{lines[yPos][xPos]}"));

            if (!added)
                return [];

            prevYPos = yPos;
            prevXPos = xPos;

            if (yPos-1 < 0 || yPos+1 >= lines.Count() || xPos-1 < 0 || xPos+1 >= lines[yPos].Length)
                break;
            else if (lines[yPos][xPos] == '^' && lines[yPos-1][xPos] == '#')
                lines[yPos] = lines[yPos].Remove(xPos, 1).Insert(xPos, ">");
            else if (lines[yPos][xPos] == '>' && lines[yPos][xPos+1] == '#')
                lines[yPos] = lines[yPos].Remove(xPos, 1).Insert(xPos, "v");
            else if (lines[yPos][xPos] == 'v' && lines[yPos+1][xPos] == '#')
                lines[yPos] = lines[yPos].Remove(xPos, 1).Insert(xPos, "<");
            else if (lines[yPos][xPos] == '<' && lines[yPos][xPos-1] == '#')
                lines[yPos] = lines[yPos].Remove(xPos, 1).Insert(xPos, "^");
            else if (lines[yPos][xPos] == '^')
                yPos--;
            else if (lines[yPos][xPos] == '>')
                xPos++;
            else if (lines[yPos][xPos] == 'v')
                yPos++;
            else if (lines[yPos][xPos] == '<')
                xPos--;

            if (prevYPos != yPos || prevXPos != xPos)
            {
                lines[yPos] = lines[yPos].Remove(xPos, 1).Insert(xPos, $"{lines[prevYPos][prevXPos]}");
                lines[prevYPos] = lines[prevYPos].Remove(prevXPos, 1).Insert(prevXPos, "X");
            }
        }

        result.Add((prevYPos, prevXPos, $"{lines[prevYPos][prevXPos]}"));

        return result;
    }

    private static void Part2()
    {
        var filename = "puzzleInput.txt";

        var lines = File.ReadAllLines(filename).ToList();

        var startingPosLine = lines.First(l => l.Contains('^'));
        var yPos = lines.IndexOf(startingPosLine);
        var xPos = startingPosLine.IndexOf("^");

        var traverseResult = Traverse(lines.ToArray(), yPos, xPos).Select(r => (r.Item1, r.Item2)).ToList();

        var distinctResults = traverseResult.Distinct();

        var counter = 0;
        foreach (var distinctResult in distinctResults)
        {
            if (distinctResult.Item1 == yPos && distinctResult.Item2 == xPos)
                continue;

            var newLines = lines.ToArray();
            newLines[distinctResult.Item1] = newLines[distinctResult.Item1].Remove(distinctResult.Item2, 1).Insert(distinctResult.Item2, "#");

            if (!Traverse(newLines, yPos, xPos).Any())
                counter++;
        }

        Console.WriteLine($"counter = {counter}");
    }
}
