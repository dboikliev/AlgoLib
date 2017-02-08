using DataStructures.Trees;
using System;

namespace DataStructures.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var trie = new Trie();
            trie.Insert("Romanus");
            trie.Insert("Romulus");
            Console.WriteLine(trie.IsWord("Romanus"));
            Console.WriteLine(trie.IsWord("Romanu"));
            Console.WriteLine(trie.IsPrefix("Romanus"));
            Console.WriteLine(trie.IsWord("Romulus"));
            trie.Remove("Romulus");
            Console.WriteLine(trie.IsWord("Romulus"));
            Console.WriteLine(trie.IsPrefix("Romulus"));
        }
    }
}
