using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Collections
{
    public class KdTree<T>
    {
        private class KdTreeNode
        {
            public T[] Value { get; }
            
            public KdTreeNode Left { get; set; }
            public KdTreeNode Right { get; set; }

            public KdTreeNode(T[] value)
            {
                Value = value;
            }
        }

        private readonly Comparison<T> _comparison;
        private readonly int _k;
        private readonly KdTreeNode _root;

        public KdTree(Comparison<T> comparison, IEnumerable<T[]> elements, int k)
        {
            _comparison = comparison;
            _k = k;
            _root = ConstructTree(elements, k, 0);
        }
        
        public KdTree(IEnumerable<T[]> elements, int k) 
            : this(Comparer<T>.Default.Compare, elements, k)
        {
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
            KdTreeNode current = _root;
            KdTreeNode parent = current;

            while (current != null)
            {
                int comparison = _comparison(current.Value[axis], element[axis]);

                current = comparison <= 0 ? current.Left : current.Right;
                
                parent = current;

                depth++;
                axis = depth % _k;
            }

            if (_comparison(parent.Value[axis], element[axis]) <= 0)
            {
                parent.Left = new KdTreeNode(element);
            }
            else
            {
                parent.Right = new KdTreeNode(element);
            }
        }

        private KdTreeNode ConstructTree(IEnumerable<T[]> elements, int k, int depth)
        {
            int axis = depth % k;

            T[][] sorted = elements.OrderBy(x => x[axis]).ToArray();

            if (sorted.Length == 0)
            {
                return null;
            }
            
            T[] median = sorted[sorted.Length / 2];

            var node = new KdTreeNode(median)
            {
                Left = ConstructTree(new ArraySegment<T[]>(sorted, 0, sorted.Length / 2), k, depth + 1),
                Right = ConstructTree(new ArraySegment<T[]>(sorted, sorted.Length / 2, sorted.Length / 2), k,
                    depth + 1)
            };

            return node;
        }
    }
}