using System;
using System.Collections.Generic;
using System.Linq;
using AlgoLib.Collections;
using Xunit;

namespace AlgoLib.Tests
{
    public class PriorityDequeTests
    {
        [Fact]
        public void Enqueue_ShouldIncreaseCounBy1_WhenCalled()
        {
            var pd = new PriorityDeque<int>((a, b) => a - b);
            
            foreach (var i in Enumerable.Range(1, 100))
            {
                pd.Enqueue(i);
                Assert.Equal(i, pd.Count);
            }
        }
        
        [Fact]
        public void Deque_ShouldDecreseCounBy1_WhenCalled()
        {
            var pd = new PriorityDeque<int>((a, b) => a - b);

            var count = 100;
            foreach (var i in Enumerable.Range(1, count))
            {
                pd.Enqueue(i);
            }

            for (int i = 0; pd.Count > 0; i++)
            {
                Assert.Equal(count - i, pd.Count);
                pd.DequeueMin();
            }
            
            pd = new PriorityDeque<int>((a, b) => a - b);

            foreach (var i in Enumerable.Range(1, count))
            {
                pd.Enqueue(i);
            }
            
            for (int i = 0; pd.Count > 0; i++)
            {
                Assert.Equal(count - i, pd.Count);
                pd.DequeueMax();
            }
        }
        
        [Fact]
        public void DequeueMax_ShouldReturnValuesInDescnedingOrder_WhenMinQueue()
        {
            var pd = new PriorityDeque<int>((a, b) => a - b);
            var elements = new[] {1, 10, 2, 8, 3, 7, 4, 6, 5};
            var dequeued = new List<int>();

            foreach (var element in elements)
            {
                pd.Enqueue(element);
            }

            while (pd.Count > 0)
            {
                dequeued.Add(pd.DequeueMax());
            }
            
            Assert.Equal(elements.OrderByDescending(x => x), dequeued);
        }

        [Fact]
        public void DequeueMin_ShouldReturnValuesInAscendingOrder_WhenMinQueue()
        {
            var pd = new PriorityDeque<int>((a, b) => a - b);
            var dequeued = new List<int>();
            var elements = new[] {1, 10, 2, 8, 3, 7, 4, 6, 5};
             
            foreach (var element in elements)
            {
                pd.Enqueue(element);
            }
            
            while (pd.Count > 0)
            {
                dequeued.Add(pd.DequeueMin());
            }
            
            Assert.Equal(elements.OrderBy(x => x), dequeued);
        }
    }
}