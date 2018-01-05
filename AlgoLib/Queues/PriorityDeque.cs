using System;
using System.Security.Cryptography.X509Certificates;

namespace AlgoLib.Queues
{
    public class PriorityDeque<T>
    {
        private class PriorityDequeNode<K>
        {
            public K Min;
            public K Max;

            public PriorityDequeNode(K minMax) => Min = Max = minMax;
        }
        
        private const int InitialCapacity = 16;

        private readonly Comparison<T> _comparison;
        private PriorityDequeNode<T>[] _elements;
        private int _pointer = 1;

        public int Count { get; private set; } = 0;

        public PriorityDeque(Comparison<T> comparison, int capacity = InitialCapacity)
        {
            _comparison = comparison;
            _elements = new PriorityDequeNode<T>[capacity];
        }

        public T DequeueMin()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }
         
            var result = _elements[1].Min;
            
            Count--;

            _elements[1].Min = _elements[_pointer].Min;

            SiftDownMin();

            if (Count % 2 == 0)
            {
                if (_pointer > 1)
                {
                    _elements[_pointer] = null;
                    _pointer--;
                }
            }
            else
            {
                _elements[_pointer].Min = _elements[_pointer].Max;
            }

            return result;
        }

        public T DequeueMax()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }
            
            var result = _elements[1].Max;

            _elements[1].Max = _elements[_pointer].Max;

            SiftDownMax();
            
            Count--;

            if (Count % 2 == 0)
            {
                if (_pointer > 1)
                {
                    _elements[_pointer] = null;
                    _pointer--;
                }
            }
            else
            {
                _elements[_pointer].Max = _elements[_pointer].Min;
            }
            
            return result;
        }

        public T PeekMin()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _elements[1].Min;
        }

        public T PeekMax()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _elements[1].Max;
        }
        
        private void SiftDownMin()
        {
            var isRunning = true;
            var current = 1;
            while (isRunning)
            {
                var original = current;
                var leftIndex = 2 * current;
                var rightIndex = 2 * current + 1;

                if (leftIndex <= _pointer && _comparison(_elements[current].Min, _elements[leftIndex].Min) > 0)
                {
                    current = leftIndex;
                }
                
                if (rightIndex <= _pointer && _comparison(_elements[current].Min, _elements[rightIndex].Min) > 0)
                {
                    current = rightIndex;
                }

                isRunning = current != original;
                if (isRunning)
                {
                    var temp = _elements[current].Min;
                    _elements[current].Min = _elements[original].Min;
                    _elements[original].Min = temp;
                }
            }
        }

        private void SiftDownMax()
        {
            var isRunning = true;
            var current = 1;
            while (isRunning)
            {
                var original = current;
                var leftIndex = 2 * current;
                var rightIndex = 2 * current + 1;

                if (leftIndex <= _pointer && _comparison(_elements[current].Max, _elements[leftIndex].Max) < 0)
                {
                    current = leftIndex;
                }
                
                if (rightIndex <= _pointer && _comparison(_elements[current].Max, _elements[rightIndex].Max) < 0)
                {
                    current = rightIndex;
                }

                isRunning = current != original;
                if (isRunning)
                {
                    var temp = _elements[current].Max;
                    _elements[current].Max = _elements[original].Max;
                    _elements[original].Max = temp;
                }
            }
        }

        public PriorityDeque<T> Enqueue(T value)
        {   
            if (Count >= 2 && Count % 2 == 0)
                _pointer++;
            
            if (_pointer == _elements.Length)
            {
                Array.Resize(ref _elements, _elements.Length * 2);
            }
            
            if (Count % 2 == 0)
            {
                _elements[_pointer] = new PriorityDequeNode<T>(value);
                
                if (_pointer > 1)
                {
                    if (_comparison(_elements[_pointer].Min, _elements[_pointer / 2].Min) < 0)
                        MinHeapify();
                    else if (_comparison(_elements[_pointer].Max, _elements[_pointer / 2].Max) > 0)
                        MaxHeapify();
                }
                
            }
            else
            {
                var node = _elements[_pointer];

                if (_comparison(node.Max, value) >= 0)
                {
                    node.Min = value;
                    MinHeapify();
                }
                else if (_comparison(node.Min, value) <= 0)
                {
                    node.Max = value;
                    MaxHeapify();
                }
            }

            Count++;
            
            return this;
        }

        private void MaxHeapify()
        {
            var current = _pointer;

            while (current > 1 && _comparison(_elements[current].Max, _elements[current / 2].Max) > 0)
            {
                var temp = _elements[current].Max;
                _elements[current].Max = _elements[current / 2].Max;
                _elements[current / 2].Max = temp;

                current /= 2;
            }
            
            if (Count % 2 == 0)
                _elements[_pointer].Min = _elements[_pointer].Max;
        }

        private void MinHeapify()
        {
            var current = _pointer;

            while (current > 1 && _comparison(_elements[current].Min, _elements[current / 2].Min) < 0)
            {
                var temp = _elements[current].Min;
                _elements[current].Min = _elements[current / 2].Min;
                _elements[current / 2].Min = temp;

                current /= 2;
            }

            if (Count % 2 == 0)
                _elements[_pointer].Max = _elements[_pointer].Min;
        }
    }
}