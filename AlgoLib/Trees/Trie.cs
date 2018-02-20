using System;
using System.Collections.Generic;

namespace AlgoLib.Trees
{
    public class Trie
    {
        private class TrieNode
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
        
        private TrieNode _root;

        public Trie()
        {
            _root = new TrieNode();
        }

        public bool IsPrefix(string key)
        {
            return FindNode(key) != null;
        }

        public bool IsWord(string key)
        {
            return FindNode(key)?.IsWord ?? false;
        }

        public void Remove(string key)
        {
            var node = FindNode(key);

            if (!(node?.IsWord ?? false))
            {
                throw new Exception($"{ key } is not a word.");
            }

            var parent = node.Parent;
            var nodeToDelete = node;
            while (parent != _root && parent.Children.Count <= 1)
            {
                nodeToDelete = parent;
                parent = parent.Parent;
            }

            parent.Children.Remove(nodeToDelete.Key);
        }

        private TrieNode FindNode(string key)
        {
            var node = _root;
            for (int i = 0; i < key.Length; i++)
            {
                TrieNode child;
                if (node.Children.TryGetValue(key[i], out child))
                {
                    node = child;
                }
                else
                {
                    return null;
                }
            }
            return node;
        }

        public void Insert(string value)
        {
            var node = _root;
            int index = 0;

            while (index < value.Length)
            {
                TrieNode child;
                if (node.Children.TryGetValue(value[index], out child))
                {
                    node = child;
                }
                else
                {
                    break;
                }

                index++;
            }

            while (index < value.Length)
            {
                var child = new TrieNode();
                child.Key = value[index];
                node.Children[value[index]] = child;
                child.Parent = node;
                node = child;
                index++;
            }
            node.IsWord = true;
        }
    }

}
