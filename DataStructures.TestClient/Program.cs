using DataStructures.DisjointSet;
using System;

namespace DataStructures.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var set = new DisjointSet<int>();
            set.Find(100);
            for (int i = 0; i < 50; i++)
            {
                set.MakeSet(i);
                set.Union(0, i);
            }

            Console.WriteLine(set.Find(5) == set.Find(20));
        }
    }
}
