using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AlgoLib.Trees
{
    public class KdTree<T> where T : IComparable<T>
    {
        private class KdTreeNode<K>
        {
            public K[] Value { get; }
            
            public KdTreeNode<K> Left { get; set; }
            public KdTreeNode<K> Right { get; set; }

            public KdTreeNode(K[] value)
            {
                Value = value;
            }
        }

        private readonly int _k;
        private readonly KdTreeNode<T> _root;
        
        public KdTree(IEnumerable<T[]> elements, int k)
        {
            _k = k;
            _root = ConstructTree(elements, k, 0);
        }

        public void Add(T[] element)
        {
            Add(element, 0);
        }

        public T[][] FindNearest(T[] element, Func<T[], T[], int> distanceFunc)
        {
            throw new NotImplementedException();
        }

        private void Add(T[] element, int depth)
        {
            int axis = depth % _k;
            KdTreeNode<T> current = _root;
            KdTreeNode<T> parent = current;

            while (current != null)
            {
                int comparison = current.Value[axis].CompareTo(element[axis]);

                current = comparison <= 0 ? current.Left : current.Right;
                
                parent = current;

                depth++;
                axis = depth % _k;
            }

            if (parent.Value[axis].CompareTo(element[axis]) <= 0)
            {
                parent.Left = new KdTreeNode<T>(element);
            }
            else
            {
                parent.Right = new KdTreeNode<T>(element);
            }
        }

        private KdTreeNode<T> ConstructTree(IEnumerable<T[]> elements, int k, int depth)
        {
            int axis = depth % k;

            T[][] sorted = elements.OrderBy(x => x[axis]).ToArray();

            if (sorted.Length == 0)
            {
                return null;
            }
            
            T[] median = sorted[sorted.Length / 2];

            var node = new KdTreeNode<T>(median)
            {
                Left = ConstructTree(new ArraySegment<T[]>(sorted, 0, sorted.Length / 2), k, depth + 1),
                Right = ConstructTree(new ArraySegment<T[]>(sorted, sorted.Length / 2, sorted.Length / 2), k,
                    depth + 1)
            };

            return node;
        }
    }
}