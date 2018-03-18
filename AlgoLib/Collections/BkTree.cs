using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Collections
{
    /// <summary>
    /// A function that defines a distance between each pair of elements of a space.
    /// From <see cref="!:https://en.wikipedia.org/wiki/Metric_(mathematics)"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the set.</typeparam>
    /// <param name="a">First element.</param>
    /// <param name="b">Second element.</param>
    /// <returns>Metric distance between the two elements.</returns>
    public delegate int Metric<in T>(T a, T b);

    /// <summary>
    /// Burkhard-Keller tree. Suitable for elements which form a metric spece.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public class BkTree<T>
    {
        private class BkTreeNode
        {
            public T Value { get; }

            public Dictionary<int, BkTreeNode> Children { get; } = new Dictionary<int, BkTreeNode>();

            public BkTreeNode(T value) => Value = value;
        }

        private readonly Metric<T> _metric;
        private BkTreeNode _root;

        /// <summary>
        /// Constructs a Bukhard-Keller tree.
        /// </summary>
        /// <param name="metric">A function which is a metric on <typeparamref name="T"/>.</param>
        public BkTree(Metric<T> metric) :
            this(metric, Enumerable.Empty<T>())
        {
        }

        /// <summary>
        /// Constructs a Bukhard-Keller tree.
        /// </summary>
        /// <param name="metric">A function which is a metric on <typeparamref name="T"/>.</param>
        /// <param name="elements">Elements to be added to the tree.</param>
        public BkTree(Metric<T> metric, params T[] elements) :
            this(metric, elements as IEnumerable<T>)
        {
        }

        /// <summary>
        /// Constructs a Bukhard-Keller tree.
        /// </summary>
        /// <param name="metric">A function which is a metric on <typeparamref name="T"/>.</param>
        /// <param name="elements">Elements to be added to the tree.</param>
        public BkTree(Metric<T> metric, IEnumerable<T> elements)
        {
            _metric = metric;
            foreach (var element in elements)
            {
                Add(element);
            }
        }



        /// <summary>
        /// Adds an element to the tree.
        /// </summary>
        /// <param name="value">The element to add.</param>
        /// <returns>The tree.</returns>
        public BkTree<T> Add(T value)
        {
            if (_root == null)
            {
                _root = new BkTreeNode(value);
            }
            else
            {
                BkTreeNode current = _root;
                int distance;
                while (current.Children.TryGetValue(distance = _metric(current.Value, value), out BkTreeNode child))
                {
                    current = child;
                }

                current.Children[distance] = new BkTreeNode(value);
            }

            return this;
        }

        /// <summary>
        /// Queries the tree for elements which are not further than
        /// <paramref name="maxDistance"/> from <paramref name="value"/>
        /// </summary>
        /// <param name="value">The element to be queried.</param>
        /// <param name="maxDistance">The maximum allowed distance for the returned elements.</param>
        /// <returns>The elements within distance of <paramref name="maxDistance"/> from <paramref name="value"/>.</returns>
        public IEnumerable<T> Query(T value, int maxDistance)
        {
            var queue = new Queue<BkTreeNode>();
            queue.Enqueue(_root);

            while (queue.Count > 0)
            {
                BkTreeNode current = queue.Dequeue();

                int distance = _metric(value, current.Value);
                if (distance <= maxDistance)
                {
                    yield return current.Value;
                }
                
                IEnumerable<BkTreeNode> potentialChildren = current.Children
                    .Where(k => k.Key >= distance - maxDistance &&
                                k.Key <= distance + maxDistance)
                    .Select(k => k.Value);

                foreach (BkTreeNode child in potentialChildren)
                {
                    queue.Enqueue(child);
                }
            }
        }
    }
}