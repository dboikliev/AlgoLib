namespace DataStructures.Sets
{
    public class DisjointSetNode<T>
    {
        public DisjointSetNode<T> Parent { get; set; }
        public int Rank { get; set; } = 1;
        public T Value { get; }

        public DisjointSetNode(T value)
        {
            Value = value;
        }
    }
}