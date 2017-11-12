using System;
using AlgoLib.Trees;
using static AlgoLib.Functions.EditDistance;

namespace AlgoLib.TestClient
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var tree = new BKTree<string>(LevensteinDistance, "kitten", "mitten", "smitten");

            foreach (var str in tree.Query("smitten", 3))
            {
                Console.WriteLine(str);
            }
        }
    }
}