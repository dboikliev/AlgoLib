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
        private int _count = 0;

        public int Count => _count;

        public Deque(int capacity = InitialCapacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, $"{nameof(capacity)} must be a positive number.");
            
            _elements = new T[capacity];
        }
        
        public Deque<T> EnqueueFirst(T value)
        {
            if (_count + 1 >= _elements.Length)
                Resize();
            
            if (_count > 0)
            {
                _head--;
                if (_head < 0)
                {
                    _head = _elements.Length - 1;
                }
            }
                
            _elements[_head] = value;
            _count++;

            return this;
        }

        public T DequeueFirst()
        {
            if (_count == 0)
                throw new InvalidOperationException("The queue is empty.");

            var result = _elements[_head];
            
            if (_count > 0)
                _head = (_head + 1) % _elements.Length;
            
            _count--;
            return result;
        }
        
        public Deque<T> EnqueueLast(T value)
        {
            
            if (_count + 1 >= _elements.Length)
                Resize();

            if (_count > 0)
                _tail = (_tail + 1) % _elements.Length;
            
            _elements[_tail] = value;
            _count++;

            return this;
        }

        public T DequeueLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            var result = _elements[_tail];

            _count--;
            _tail--;
            if (_tail < 0)
            {
                _tail = _elements.Length - 1;
            }

            return result;
        }

        private void Resize()
        {
            var resized = new T[_elements.Length * 2];

            Array.ConstrainedCopy(_elements, _head, resized, 0, _elements.Length - _head);
            Array.ConstrainedCopy(_elements, 0, resized, _elements.Length - _head, _head);

            _elements = resized;

            _head = 0;
            _tail = _count - 1;
        }
    }
}