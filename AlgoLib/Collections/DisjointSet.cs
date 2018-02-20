using System;
using System.Collections.Generic;

namespace AlgoLib.Collections
{
    public class DisjointSet<T>
    {
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

        public DisjointSetNode<T> Find(T value)
        {
            DisjointSetNode<T> current;

            if (_nodes.TryGetValue(value, out current))
            {
                if (current != current.Parent)
                {
                    current.Parent = Find(current.Parent.Value);
                }
                return current.Parent;
            }
            return null;
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
    }
}