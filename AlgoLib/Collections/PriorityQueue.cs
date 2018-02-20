using System;
using AlgoLib.Extensions;

namespace AlgoLib.Collections
{
    public class PriorityQueue<T>
    {
        private readonly Comparison<T> _comparison;
        private const int InitialCapacity = 16;
        private T[] _elements;
        private int _pointer = 1;

        public int Count => _pointer - 1;

        public PriorityQueue(Comparison<T> comparison, int capacity = InitialCapacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException
                (
                    nameof(capacity), 
                    capacity, 
                    $"{nameof(capacity)} must be a positive number."
                );
            }
            
            _comparison = comparison;
            _elements = new T[capacity];
        }

        public PriorityQueue<T> Enqueue(T element)
        {
            var currentIndex = _pointer;

            _elements[currentIndex] = element;
            while (currentIndex > 1 && _comparison(_elements[currentIndex], _elements[currentIndex / 2]) < 0)
            {
                _elements.Swap(currentIndex, currentIndex / 2);
                currentIndex /= 2;
            }
            
            _pointer++;
            
            if (_pointer == _elements.Length)
                Array.Resize(ref _elements, _elements.Length * 2);
            
            return this;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }
            
            return _elements[1];
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }
            
            var result = _elements[1];
            _pointer--;

            var last = _elements[_pointer];
            _elements[1] = last;

            var current = 1;
            
            var isRunning = true;
            while (isRunning)
            {
                var original = current;
                var leftIndex = current * 2;
                var rightIndex = current * 2 + 1;

                if (leftIndex < _pointer && _comparison(_elements[leftIndex], _elements[current]) < 0)
                {
                    current = leftIndex;
                }
                
                if (rightIndex < _pointer && _comparison(_elements[rightIndex], _elements[current]) < 0)
                {
                    current = rightIndex;
                }

                isRunning = current != original;
                if (isRunning)
                {
                    _elements.Swap(current, original);
                }
            }

            return result;
        }
    }
}