using System;
using System.Collections.Generic;
using System.Linq;
using AlgoLib.Queues;
using Xunit;

namespace AlgoLib.Tests
{
    public class DequeTests
    {
        [Fact]
        public void Count_ShouldBe0_ForEmptyQueue()
        {
            var deque = new Deque<int>();
            
            Assert.Equal(0, deque.Count);
        }
        
        [Fact]
        public void DequeueFirst_ShouldThrowException_ForEmptyQueue()
        {
            var deque = new Deque<int>();

            var ex = Assert.Throws<InvalidOperationException>(() => deque.DequeueFirst());
            
            Assert.Equal("The queue is empty.", ex.Message);
        }
        
        [Fact]
        public void DequeueLast_ShouldThrowException_ForEmptyQueue()
        {
            var deque = new Deque<int>();

            var ex = Assert.Throws<InvalidOperationException>(() => deque.DequeueLast());
            
            Assert.Equal("The queue is empty.", ex.Message);
        }
        
        [Fact]
        public void DequeueFirstAfterEnqueueLast_ShouldReturnElementsInOrderOfEnqueueing()
        {
            var deque = new Deque<int>();

            var numbers = Enumerable.Range(1, 60).ToArray();
            foreach (var x in numbers)
            {
                deque.EnqueueLast(x);
            }

            var elements = new List<int>();

            while (deque.Count > 0)
            {
                elements.Add(deque.DequeueFirst());
            }
            
            Assert.Equal(numbers, elements);
        }
        
        [Fact]
        public void DequeueLastAfterEnqueueLast_ShouldReturnElementsInReverseOrderOfEnqueueing()
        {
            var deque = new Deque<int>();

            var numbers = Enumerable.Range(1, 60).ToArray();
            foreach (var x in numbers)
            {
                deque.EnqueueLast(x);
            }

            var elements = new List<int>();

            while (deque.Count > 0)
            {
                elements.Add(deque.DequeueLast());
            }
            
            Assert.Equal(numbers.Reverse(), elements);
        }
        
        [Fact]
        public void DequeueFirstAfterEnqueueFirst_ShouldReturnElementsInReverseOrderOfEnqueueing()
        {
            var deque = new Deque<int>();

            var numbers = Enumerable.Range(1, 60);
            foreach (var x in numbers)
            {
                deque.EnqueueFirst(x);
            }

            var elements = new List<int>();

            while (deque.Count > 0)
            {
                elements.Add(deque.DequeueFirst());
            }
            
            Assert.Equal(numbers.Reverse(), elements);
        } 
        
        [Fact]
        public void DequeueLastAfterEnqueueFirst_ShouldReturnElementsInOrderOfEnqueueing()
        {
            var deque = new Deque<int>();

            var numbers = Enumerable.Range(1, 60);
            foreach (var x in numbers)
            {
                deque.EnqueueFirst(x);
            }

            var elements = new List<int>();

            while (deque.Count > 0)
            {
                elements.Add(deque.DequeueLast());
            }
            
            Assert.Equal(numbers, elements);
        }
        
        [Fact]
        public void Count_ShouldBeSameAsEnqueuedNumberOfElementsAfterEnqueueFirst()
        {
            var deque = new Deque<int>();

            var numbers = Enumerable.Range(1, 60);
            foreach (var x in numbers)
            {
                deque.EnqueueFirst(x);
            }

            Assert.Equal(numbers.Count(), deque.Count);
        }
        
        [Fact]
        public void Count_ShouldBeSameAsEnqueuedNumberOfElementsAfterEnqueueLast()
        {
            var deque = new Deque<int>();

            var numbers = Enumerable.Range(1, 60);
            foreach (var x in numbers)
            {
                deque.EnqueueLast(x);
            }

            Assert.Equal(numbers.Count(), deque.Count);
        }

    }
}