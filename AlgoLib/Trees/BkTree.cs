using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Trees
{
    public delegate int Metric<T>(T a, T b);
    
    /// <summary>
    /// Burkhard-Keller tree. Suitable for elements which for a metric spece.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public class BkTree<T>
    {
        private readonly Metric<T> _metric;
        private BkTreeNode<T> _root;

        /// <summary>
        /// Constructs a Bukhard-Keller tree.
        /// </summary>
        /// <param name="metric">A function which is a metric on <typeparamref name="T"/>.</param>
        public BkTree(Metric<T> metric): 
            this(metric, Enumerable.Empty<T>())
        {
        }
        
        /// <summary>
        /// Constructs a Bukhard-Keller tree.
        /// </summary>
        /// <param name="metric">A function which is a metric on <typeparamref name="T"/>.</param>
        /// <param name="elements">Elements to be added to the tree.</param>
        public BkTree(Metric<T> metric, params T[] elements): 
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
                _root = new BkTreeNode<T>(value, _metric);
            else
                _root.Add(value);

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
            return _root.Query(value, maxDistance);
        }
    }
}