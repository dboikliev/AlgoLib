using System;
using System.Collections.Generic;

namespace AlgoLib.Collections
{
    public class DisjointSet<T>
    {
        private class DisjointSetNode
        {
            public DisjointSetNode Parent { get; set; }
            public int Rank { get; set; } = 1;
            public T Value { get; }

            public DisjointSetNode(T value)
            {
                Value = value;
            }
        }

        private readonly Dictionary<T, DisjointSetNode> _nodes = new Dictionary<T, DisjointSetNode>();

        public void MakeSet(T value)
        {
            if (_nodes.ContainsKey(value))
            {
                throw new Exception("Set already exists");
            }

            var node = new DisjointSetNode(value);
            node.Parent = node;
            _nodes[value] = node;
        }

        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        public void Union(T first, T second)
        {
            var firstRoot = Find(first);
            var secondRoot = Find(second);

            if (firstRoot == secondRoot)
            {
                return;
            }

            if (firstRoot.Rank < secondRoot.Rank)
            {
                firstRoot.Parent = secondRoot;
                secondRoot.Rank += secondRoot.Rank;
            }
            else
            {
                secondRoot.Parent = firstRoot;
                firstRoot.Rank += secondRoot.Rank;
            }
        }

        private DisjointSetNode Find(T value)
        {
            if (_nodes.TryGetValue(value, out DisjointSetNode current))
            {
                if (current != current.Parent)
                {
                    current.Parent = Find(current.Parent.Value);
                }
                return current.Parent;
            }

            return null;
        }
    }
}