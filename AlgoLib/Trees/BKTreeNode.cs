using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Trees
{
    public class BKTreeNode<T>
    {
        private readonly Metric<T> _metric;
        private T Value { get; }

        private SortedDictionary<int, BKTreeNode<T>> _children = new SortedDictionary<int, BKTreeNode<T>>();
        
        public BKTreeNode(T value, Metric<T> metric)
        {
            _metric = metric;
            Value = value;
        }

        public void Add(T value)
        {
            var current = this;
            var distance = _metric(current.Value, value);
            while (current._children.TryGetValue(distance, out var child))
            {
                current = child;
            }
            
            current._children[distance] = new BKTreeNode<T>(value, _metric);
        }
        
        public IEnumerable<T> Query(T value, int maxDistance)
        {
            var queue = new Queue<BKTreeNode<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                BKTreeNode<T> current = queue.Dequeue();

                if (_metric(value, current.Value) <= maxDistance)
                {
                    yield return current.Value;
                    IEnumerable<BKTreeNode<T>> potentialChildren = current._children
                        .Where(k => k.Key <= _metric(value, current.Value) + maxDistance ||
                                    k.Key >= _metric(value, current.Value) - maxDistance)
                        .Select(k => k.Value);
                    
                    foreach (BKTreeNode<T> child in potentialChildren)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }
    }
}