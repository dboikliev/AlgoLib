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

            tree.Add("tree").Add("fee").Add("tee").Add("party").Add("smarty");

            var query = "smite";
            foreach (var str in tree.Query(query, 4))
            {
                Console.WriteLine(str);
                Console.WriteLine(LevensteinDistance(query, str));
            }
        }
    }
}