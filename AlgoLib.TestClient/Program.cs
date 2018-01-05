using System;
using AlgoLib.Queues;
using AlgoLib.Trees;

namespace AlgoLib.TestClient
{
    internal static class Program
    {
        private static void Main()
        {
            var pd = new PriorityDeque<int>((a, b) => a - b);

            pd.Enqueue(3).Enqueue(20).Enqueue(1);

            Console.WriteLine(pd.DequeueMax());
            Console.WriteLine(pd.DequeueMax());
            Console.WriteLine(pd.DequeueMax());
            Console.WriteLine(pd.DequeueMax());
            
            KdTree<int> points = new KdTree<int>(new []
            {
                new[] { 1, 2 }, 
                new[] { 2, 2 },
                new[] { 3, 2 }, 
                new[] { 5, 4 }
            }, 2);
        }
    }
}