namespace Day1;

class Program
{
    static void Main(string[] args)
    {
        //Part1();
        Part2();
    }

    static void Part1()
    {
        string targetFileName = "puzzleInput.txt";

        var lines = File.ReadAllLines(targetFileName);

        List<int> listLeft = [];
        List<int> listRight = [];

        foreach (var line in lines)
        {
            var lineParts = line.Split("   ", StringSplitOptions.TrimEntries);

            var num1 = int.Parse(lineParts[0]);
            var num2 = int.Parse(lineParts[1]);

            listLeft.Add(num1);
            listRight.Add(num2);
        }

        listLeft.Sort();
        listRight.Sort();

        if (listLeft.Count != listRight.Count)
        {
            Console.Error.WriteLine("Incorrect file structure");
            Environment.Exit(-1);
        }

        int totalDistance = 0;

        for (int i = 0; i < listLeft.Count; i++)
            totalDistance += Math.Abs(listLeft[i] - listRight[i]);

        Console.WriteLine($"totalDistance: {totalDistance}");
    }

    static void Part2()
    {
        string targetFileName = "example.txt";

        var lines = File.ReadAllLines(targetFileName);

        List<int> listLeft = [];

        Dictionary<int, int> dictRight = [];

        foreach (var line in lines)
        {
            var lineParts = line.Split("   ", StringSplitOptions.TrimEntries);

            var num1 = int.Parse(lineParts[0]);
            var num2 = int.Parse(lineParts[1]);

            listLeft.Add(num1);

            if (dictRight.ContainsKey(num2))
                dictRight[num2] += 1;
            else
                dictRight.Add(num2, 1);
        }

        int totalSimilarityScore = 0;

        foreach (var num in listLeft)
            if (dictRight.ContainsKey(num))
                totalSimilarityScore += num * dictRight[num];

        Console.WriteLine($"totalSimilarityScore: {totalSimilarityScore}");
    }
}
