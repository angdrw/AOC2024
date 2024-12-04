using System.Text;

namespace Day4;

class Program
{
    static void Main(string[] args)
    {
        // Part1();
        Part2();
    }

    private static bool IsTargetWord(string word, string targetWord)
    {
        return word.Equals(targetWord, StringComparison.InvariantCultureIgnoreCase) ||
                     word.Equals(new string(targetWord.Reverse().ToArray()), StringComparison.InvariantCultureIgnoreCase);
    }

    static void Part1()
    {
        string filename = "puzzleInput.txt";

        var lines = File.ReadAllLines(filename);

        var targetWord = "XMAS";
        var targetWordCount = targetWord.Count();

        int occurrences = 0;

        for (int i = 0; i < lines.Count(); i++)
        {
            for (int y = 0; y < lines[i].Count(); y++)
            {
                if (y + targetWordCount <= lines[i].Count())
                {
                    var rightWord = lines[i][y..(y+targetWordCount)];

                    if (IsTargetWord(rightWord, targetWord))
                        occurrences++;
                }

                if (i + targetWordCount <= lines.Count())
                {
                    StringBuilder tempDownWord = new();

                    for (int z = i; z < i + targetWordCount; z++)
                        tempDownWord.Append(lines[z][y]);

                    string downWord = tempDownWord.ToString();

                    if (IsTargetWord(downWord, targetWord))
                        occurrences++;
                }

                if (i + targetWordCount <= lines.Count() && y - (targetWordCount-1) >= 0)
                {
                    StringBuilder tempDiagonallyLeftDownWord = new();

                    for (int z = i, x = y; z < i + targetWordCount && x > y - targetWordCount; z++, x--)
                        tempDiagonallyLeftDownWord.Append(lines[z][x]);

                    string diagonallyLeftDownWord = tempDiagonallyLeftDownWord.ToString();

                    if (IsTargetWord(diagonallyLeftDownWord, targetWord))
                        occurrences++;
                }

                if (i + targetWordCount <= lines.Count() && y + targetWordCount <= lines[i].Count())
                {
                    StringBuilder tempDiagonallyRightDownWord = new();

                    for (int z = i, x = y; z < i + targetWordCount && x < y + targetWordCount; z++, x++)
                        tempDiagonallyRightDownWord.Append(lines[z][x]);

                    string diagonallyRightDownWord = tempDiagonallyRightDownWord.ToString();

                    if (IsTargetWord(diagonallyRightDownWord, targetWord))
                        occurrences++;
                }
            }
        }

        Console.WriteLine($"occurrences: {occurrences}");
    }

    static void Part2()
    {
        string filename = "puzzleInput.txt";

        var lines = File.ReadAllLines(filename);

        var targetWord = "MAS";
        var targetWordCount = targetWord.Count();

        int occurrences = 0;

        for (int i = 0; i < lines.Count(); i++)
        {
            List<(int, string)> yMatchIndx = [];

            for (int y = 0; y < lines[i].Count(); y++)
            {
                if (i + targetWordCount <= lines.Count() && y - (targetWordCount-1) >= 0)
                {
                    StringBuilder tempDiagonallyLeftDownWord = new();

                    for (int z = i, x = y; z < i + targetWordCount && x > y - targetWordCount; z++, x--)
                        tempDiagonallyLeftDownWord.Append(lines[z][x]);

                    string diagonallyLeftDownWord = tempDiagonallyLeftDownWord.ToString();

                    if (IsTargetWord(diagonallyLeftDownWord, targetWord))
                        yMatchIndx.Add((y, "left"));
                }

                if (i + targetWordCount <= lines.Count() && y + targetWordCount <= lines[i].Count())
                {
                    StringBuilder tempDiagonallyRightDownWord = new();

                    for (int z = i, x = y; z < i + targetWordCount && x < y + targetWordCount; z++, x++)
                        tempDiagonallyRightDownWord.Append(lines[z][x]);

                    string diagonallyRightDownWord = tempDiagonallyRightDownWord.ToString();

                    if (IsTargetWord(diagonallyRightDownWord, targetWord))
                        yMatchIndx.Add((y, "right"));
                }
            }

            foreach (var yMatch in yMatchIndx)
                if (yMatch.Item2.Equals("right") && yMatchIndx.Contains((yMatch.Item1 + 2, "left")))
                    occurrences++;
        }

        Console.WriteLine($"occurrences: {occurrences}");
    }
}
