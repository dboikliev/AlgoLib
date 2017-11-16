using System;
using AlgoLib.Queues;
using AlgoLib.Trees;
using static AlgoLib.Functions.EditDistance;

namespace AlgoLib.TestClient
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var pd = new PriorityDeque<int>((a, b) => a - b);

            pd.Enqueue(3).Enqueue(20).Enqueue(1);

            Console.WriteLine(pd.DequeueMax());
            Console.WriteLine(pd.DequeueMax());
            Console.WriteLine(pd.DequeueMax());
        }
    }
}