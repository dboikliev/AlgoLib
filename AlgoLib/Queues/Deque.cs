using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AlgoLib.Queues
{
    public class Deque<T>
    {
        private const int InitialCapacity = 16;
        private T[] _elements;
        private int _head = 0;
        private int _tail = 0;

        public int Count { get; private set; } = 0;

        public Deque(int capacity = InitialCapacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, $"{nameof(capacity)} must be a positive number.");
            
            _elements = new T[capacity];
        }
        
        public Deque<T> EnqueueFirst(T value)
        {
            if (Count + 1 >= _elements.Length)
                Resize();
            
            if (Count > 0)
                _head = _head > 0 ? _head - 1 : _elements.Length - 1;
                
            _elements[_head] = value;
            Count++;

            return this;
        }

        public T DequeueFirst()
        {
            if (Count == 0)
                throw new InvalidOperationException("The queue is empty.");

            var result = _elements[_head];
            
            Count--;
            _head = (_head + 1) % _elements.Length;
            
            return result;
        }
        
        public Deque<T> EnqueueLast(T value)
        {
            if (Count + 1 >= _elements.Length)
                Resize();

            if (Count > 0)
                _tail = (_tail + 1) % _elements.Length;
            
            _elements[_tail] = value;
            Count++;

            return this;
        }

        public T DequeueLast()
        {
            if (Count == 0)
                throw new InvalidOperationException("The queue is empty.");

            var result = _elements[_tail];

            Count--;
            _tail = _tail > 0 ? _tail - 1 : _elements.Length - 1;

            return result;
        }

        private void Resize()
        {
            var resized = new T[_elements.Length * 2];

            Array.ConstrainedCopy(_elements, _head, resized, 0, _elements.Length - _head);
            Array.ConstrainedCopy(_elements, 0, resized, _elements.Length - _head, _head);

            _elements = resized;

            _head = 0;
            _tail = Count - 1;
        }
    }
}