using System.Collections.Generic;
using System.Linq;
using AlgoLib.Queues;
using Xunit;

namespace AlgoLib.Tests
{
    public class PriorityQueueTests
    {
        [Fact]
        public void MinQueue_ShouldDequeue_AllNumbersInAscendingOrder()
        {
            var numbers = Enumerable
                .Range(1, 20)
                .ToArray();

            var pq = numbers
                .Reverse()
                .Aggregate(new PriorityQueue<int>((a, b) => a - b), (queue, number) => queue.Enqueue(number));

            var numbersFromQueue = new List<int>();

            while (pq.Count > 0)
            {
                numbersFromQueue.Add(pq.Dequeue());
            }
            
            Assert.Equal(numbers, numbersFromQueue);
        }
        
        [Fact]
        public void MaxQueue_ShouldDequeue_AllNumbersInDescendingOrder()
        {
            var numbers = Enumerable
                .Range(1, 20)
                .ToArray();

            var pq = numbers.Aggregate(new PriorityQueue<int>((a, b) => b - a), (queue, number) => queue.Enqueue(number));

            var numbersFromQueue = new List<int>();

            while (pq.Count > 0)
            {
                numbersFromQueue.Add(pq.Dequeue());
            }
            
            Assert.Equal(numbers.OrderByDescending(x => x), numbersFromQueue);
        }
    }
}