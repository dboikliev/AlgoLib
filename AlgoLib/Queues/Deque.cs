using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AlgoLib.Queues
{
    public class Deque<T>
    {
        private const int InitialCapacity = 16;
        
        //[0, 0, 3, 4, 5, 0, 0, 0];
        private T[] _elements = new T[InitialCapacity];
        private int _head = 0;
        private int _tail = 0;
        private int _count = 0;

        public int Count => _count; 
        
        public Deque<T> EnqueueFirst(T value)
        {
            if (_count + 1>= _elements.Length)
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
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }
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

            int added = 0;
            for (int i = _head; i < _elements.Length; i++)
            {
                resized[i - _head] = _elements[i];
                added++;
            }
            
            for (int i = 0; i < _head; i++)
            {
                resized[i + added] = _elements[i];
            }

            _elements = resized;

            _head = 0;
            _tail = _count - 1;
        }
    }
}