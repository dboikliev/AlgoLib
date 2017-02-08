using System.Collections.Generic;

namespace DataStructures.Trie
{

    public class TrieNode
    {
        public bool IsWord { get; set; }
        public string Value { get; set; }
        public Dictionary<char, TrieNode> Children { get; set; }
        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
        }
    }
}
