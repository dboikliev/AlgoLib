using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trie
{
    public class Trie
    {
        private readonly TrieNode _root;

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
                node.Children[value[index]] = child;
                node = child;
                index++;
            }
            node.Value = value;
            node.IsWord = true;
        }
    }

}
