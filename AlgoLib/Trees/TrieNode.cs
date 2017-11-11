using System.Collections.Generic;

namespace DataStructures.Trees
{

    public class TrieNode
    {
        public bool IsWord { get; set; }
        public char Key { get; set; }
        public TrieNode Parent { get; set; }
        public Dictionary<char, TrieNode> Children { get; }
        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
        }
    }
}
