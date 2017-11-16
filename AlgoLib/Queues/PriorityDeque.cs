using System;

namespace AlgoLib.Queues
{
    public class PriorityDeque<T>
    {
        struct PriorityDequeNode<T>
        {
            public T Min;
            public T Max;

            public PriorityDequeNode(T minMax) => Min = Max = minMax;

            public PriorityDequeNode(T min, T max)
            {
                Min = min;
                Max = max;
            }
        }
        
        private const int InitialCapacity = 16;
        
        private PriorityDequeNode<T>[] _elements;
        
        public int Count { get; private set; };

        public PriorityDeque(int capacity = InitialCapacity)
            => _elements = new PriorityDequeNode<T>[capacity];

        public T DequeMin()
        {
            throw new NotImplementedException();
        }

        public T DequeMax()
        {
            throw new NotImplementedException();
        }

        public PriorityDeque<T> Enqueue(T value)
        {
            throw new NotImplementedException();
        }
    }
}