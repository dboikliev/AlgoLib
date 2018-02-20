using System;
using System.Collections.Generic;

namespace AlgoLib.Collections
{
    public class DisjointSet<T>
    {
        private class DisjointSetNode<T>
        {
            public DisjointSetNode<T> Parent { get; set; }
            public int Rank { get; set; } = 1;
            public T Value { get; }

            public DisjointSetNode(T value)
            {
                Value = value;
            }
        }
        
        private Dictionary<T, DisjointSetNode<T>> _nodes = new Dictionary<T, DisjointSetNode<T>>();

        public void MakeSet(T value)
        {
            if (_nodes.ContainsKey(value))
            {
                throw new Exception("Set already exists");
            }

            var node = new DisjointSetNode<T>(value);
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
        
        private DisjointSetNode<T> Find(T value)
        {
            if (_nodes.TryGetValue(value, out DisjointSetNode<T> current))
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