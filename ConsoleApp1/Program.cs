using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Tuple<string, string>> pairs = new List<Tuple<string, string>>()
        {
            Tuple.Create("A1", "A2"),
            Tuple.Create("A2", "A3"),
            Tuple.Create("A3", "A4"),
            Tuple.Create("A4", "A5"),
            Tuple.Create("A2", "A2"),
            Tuple.Create("A3", "A3"),
            Tuple.Create("A3", "A4"),
            Tuple.Create("A5", "A6"),
        };

        List<string> longestChain = FindLongestChain(pairs);

        Console.WriteLine("Максимальная цепочка: " + string.Join(", ", longestChain));
    }

    static List<string> FindLongestChain(List<Tuple<string, string>> pairs)
    {
        Dictionary<string, List<string>> adjacencyList = new Dictionary<string, List<string>>();

        foreach (var pair in pairs)
        {
            if (!adjacencyList.ContainsKey(pair.Item1))
                adjacencyList[pair.Item1] = new List<string>();

            adjacencyList[pair.Item1].Add(pair.Item2);
        }

        List<string> longestChain = new List<string>();

        foreach (var pair in pairs)
        {
            List<string> currentChain = new List<string> { pair.Item1, pair.Item2 };
            ExtendChain(adjacencyList, pair.Item2, currentChain);

            if (currentChain.Count > longestChain.Count)
                longestChain = currentChain;
        }

        return longestChain;
    }

    static void ExtendChain(Dictionary<string, List<string>> adjacencyList, string currentEnd, List<string> currentChain)
    {
        if (adjacencyList.ContainsKey(currentEnd))
        {
            foreach (var next in adjacencyList[currentEnd])
            {
                if (!currentChain.Contains(next))
                {
                    currentChain.Add(next);
                    ExtendChain(adjacencyList, next, currentChain);
                }
            }
        }
    }
}
