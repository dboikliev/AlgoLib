using System;
using AlgoLib.Trees;
using static AlgoLib.Functions.EditDistance;

namespace AlgoLib.TestClient
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var tree = new BKTree<string>(LevensteinDistance);

            tree.Add("kitten")
                .Add("mitten")
                .Add("meat");

            foreach (var str in tree.Query("smitten", 3))
            {
                Console.WriteLine(str);
            }
        }
    }
}