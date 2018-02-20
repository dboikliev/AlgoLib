using System;
using System.Collections.Generic;
using System.Linq;
using AlgoLib.Collections;
using Xunit;

namespace AlgoLib.Tests
{
    public class DequeTests
    {
        [Fact]
        public void DequeConstructor_ShouldThrowArgumentOutOfRangeException_WhenCapacityIs0()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Deque<int>(0));
        }

        [Fact]
        public void Count_ShouldBe0_ForEmptyQueue()
        {
            var deque = new Deque<int>();

            Assert.Equal(0, deque.Count);
        }

        [Fact]
        public void Count_ShouldBeSameAsEnqueuedNumberOfElementsAfterEnqueueFirst()
        {
            var deque = new Deque<int>();

            var numbers = Enumerable.Range(1, 60).ToArray();
            foreach (var x in numbers)
            {
                deque.EnqueueFirst(x);
            }

            Assert.Equal(numbers.Length, deque.Count);
        }

        [Fact]
        public void Count_ShouldBeSameAsEnqueuedNumberOfElementsAfterEnqueueLast()
        {
            var deque = new Deque<int>();

            var numbers = Enumerable.Range(1, 60).ToArray();
            foreach (var x in numbers)
            {
                deque.EnqueueLast(x);
            }

            Assert.Equal(numbers.Length, deque.Count);
        }

        [Fact]
        public void Dequeue_ShouldReturnInsertedElement_WhenCalledAfterEnqueue()
        {
            var deque = new Deque<int>();

            deque.EnqueueFirst(10);
            Assert.Equal(10, deque.DequeueFirst());

            deque.EnqueueFirst(11);
            Assert.Equal(11, deque.DequeueLast());

            deque.EnqueueLast(12);
            Assert.Equal(12, deque.DequeueFirst());

            deque.EnqueueLast(13);
            Assert.Equal(13, deque.DequeueLast());

            deque.EnqueueFirst(14).EnqueueFirst(15);
            Assert.Equal(15, deque.DequeueFirst());
            Assert.Equal(14, deque.DequeueFirst());

            deque.EnqueueFirst(16).EnqueueFirst(17);
            Assert.Equal(16, deque.DequeueLast());
            Assert.Equal(17, deque.DequeueLast());

            deque.EnqueueLast(18).EnqueueLast(19);
            Assert.Equal(18, deque.DequeueFirst());
            Assert.Equal(19, deque.DequeueFirst());

            deque.EnqueueLast(20).EnqueueLast(21);
            Assert.Equal(21, deque.DequeueLast());
            Assert.Equal(20, deque.DequeueLast());
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
        public void DequeueLast_ShouldThrowException_WhenCalledTwiceAfterEnqueueLast()
        {
            var deque = new Deque<int>();

            deque.EnqueueLast(1);
            deque.DequeueLast();

            var ex = Assert.Throws<InvalidOperationException>(() => deque.DequeueLast());

            Assert.Equal("The queue is empty.", ex.Message);
        }

        [Fact]
        public void DequeueLast_ShouldThrowException_WhenCalledTwiceAfterEnqueueFirst()
        {
            var deque = new Deque<int>();

            deque.EnqueueLast(1);
            deque.DequeueLast();

            var ex = Assert.Throws<InvalidOperationException>(() => deque.DequeueLast());

            Assert.Equal("The queue is empty.", ex.Message);
        }

        [Fact]
        public void DequeueFirst_ShouldThrowException_WhenCalledTwiceAfterEnqueueLast()
        {
            var deque = new Deque<int>();

            deque.EnqueueLast(1);
            deque.DequeueFirst();

            var ex = Assert.Throws<InvalidOperationException>(() => deque.DequeueFirst());

            Assert.Equal("The queue is empty.", ex.Message);
        }

        [Fact]
        public void DequeueFirst_ShouldThrowException_WhenCalledTwiceAfterEnqueueFirst()
        {
            var deque = new Deque<int>();

            deque.EnqueueLast(1);
            deque.DequeueFirst();

            var ex = Assert.Throws<InvalidOperationException>(() => deque.DequeueFirst());

            Assert.Equal("The queue is empty.", ex.Message);
        }

        [Fact]
        public void DequeueFirst_ShouldReturnElementsInOrderOfEnqueueing_WhenCalledAfterEnqueueLast()
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
        public void DequeueLast_ShouldReturnElementsInReverseOrderOfEnqueueing_WhenCalledAfterEnqueueLast()
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
        public void DequeueFirst_ShouldReturnElementsInReverseOrderOfEnqueueing_WhenCalledAfterEnqueueFirst()
        {
            var deque = new Deque<int>();

            var numbers = Enumerable.Range(1, 60).ToArray();
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
        public void DequeueLast_ShouldReturnElementsInOrderOfEnqueueing_WhenCalledAfterEnqueueFirst()
        {
            var deque = new Deque<int>();

            var numbers = Enumerable.Range(1, 60).ToArray();
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
    }
}