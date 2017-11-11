using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Trees
{
    public delegate int Metric<T>(T a, T b);
    
    public class BKTree<T>
    {
        private readonly Metric<T> _metric;
        private BKTreeNode<T> _root;

        public BKTree(Metric<T> metric): 
            this(metric, Enumerable.Empty<T>())
        {
        }
        
        public BKTree(Metric<T> metric, params T[] elements): 
            this(metric, elements as IEnumerable<T>)
        {
        }
        
        public BKTree(Metric<T> metric, IEnumerable<T> elements)
        {
            _metric = metric;
            foreach (var element in elements)
            {
                Add(element);
            }
        }
        
        public BKTree<T> Add(T value)
        {
            if (_root == null)
                _root = new BKTreeNode<T>(value, _metric);
            else
                _root.Add(value);

            return this;
        }

        public IEnumerable<T> Query(T value, int maxDistance)
        {
            return _root.Query(value, maxDistance);
        }
    }
}