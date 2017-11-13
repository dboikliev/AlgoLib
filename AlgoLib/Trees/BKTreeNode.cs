using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Trees
{
    /// <summary>
    /// Represents a not in a Bukhard-Kheller tree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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

        
        /// <summary>
        /// Adds a child node with a specified.
        /// </summary>
        /// <param name="value">The value of the child node.</param>
        /// <returns>The node.</returns>
        public BKTreeNode<T> Add(T value)
        {
            var current = this;
            var distance = _metric(current.Value, value);
            while (current._children.TryGetValue(distance, out var child))
            {
                current = child;
            }
            
            current._children[distance] = new BKTreeNode<T>(value, _metric);

            return this;
        }
        
        /// <summary>
        /// Queries the node and its children for elements which are within distance
        /// of <paramref name="maxDistance"/> from <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The element to be queried.</param>
        /// <param name="maxDistance">The maximum allowed distance for the returned elements.</param>
        /// <returns>The elements within distance of <paramref name="maxDistance"/> from <paramref name="value"/>.</returns>
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