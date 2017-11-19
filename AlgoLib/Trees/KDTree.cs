using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AlgoLib.Trees
{
    public class KDTree<T>
    {
        private class KDTreeNode<T>
        {
            public T[] Value { get; }
            
            public KDTreeNode<T> Left { get; set; }
            public KDTreeNode<T> Right { get; set; }

            public KDTreeNode(T[] value)
            {
                Value = value;
            }
        }

        private KDTreeNode<T> _root;
        
        public KDTree(IEnumerable<T[]> elements, int k)
        {
            _root = ConstructTree(elements, k, 0);
        }

        private KDTreeNode<T> ConstructTree(IEnumerable<T[]> elements, int k, int depth)
        {
            var axis = depth % k;

            var sorted = elements.OrderBy(x => x[axis]).ToArray();

            if (sorted.Length == 0)
            {
                return null;
            }
            
            var median = sorted[sorted.Length / 2];

            var node = new KDTreeNode<T>(median)
            {
                Left = ConstructTree(new ArraySegment<T[]>(sorted, 0, sorted.Length / 2), k, depth + 1),
                Right = ConstructTree(new ArraySegment<T[]>(sorted, sorted.Length / 2, sorted.Length / 2), k,
                    depth + 1)
            };

            return node;
        }
    }
}