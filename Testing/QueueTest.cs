using System;
using C_InANutShell.Queues;

namespace C_InANutShell.Testing
{
    public class QueueTest : ITest
    {
        public void Execute()
        {
            //RunQueueTests();
            RunPriorityQueueTests();
        }

        private void RunQueueTests()
        {
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(4);

            queue.Dequeue();
            queue.Enqueue(5);

            System.Console.WriteLine(queue.ToString());
        }

        private void RunPriorityQueueTests()
        {
            BinaryHeap<int> heap = new BinaryHeap<int>(HeapType.MinHeap);
            // heap.Insert(1);
            // System.Console.WriteLine(heap.ToString());
            // heap.Insert(200);
            // System.Console.WriteLine(heap.ToString());
            // heap.Insert(4567);
            // System.Console.WriteLine(heap.ToString());
            // heap.Insert(743);
            // System.Console.WriteLine(heap.ToString());
            // heap.Insert(32);
            // System.Console.WriteLine(heap.ToString());
            // heap.Insert(-1);
            // System.Console.WriteLine(heap.ToString());

            // for (int i = 0; i < 15; i++)
            // {
            //     heap.Insert(i);
            // }

            heap.Insert(0);
            heap.Insert(1);
            heap.Insert(2);
            heap.Insert(4);
            heap.Insert(2);
            heap.Insert(5);
            heap.Insert(3);
            
            System.Console.WriteLine(heap.ToString());
            heap.Delete(1);
            System.Console.WriteLine(heap.ToString());
            
            heap.Poll();
            System.Console.WriteLine(heap.ToString());
            heap.Poll();
            System.Console.WriteLine(heap.ToString());
            heap.Poll();
            System.Console.WriteLine(heap.ToString());
            heap.Poll();
            System.Console.WriteLine(heap.ToString());
            heap.Poll();
            System.Console.WriteLine(heap.ToString());
            heap.Poll();
            System.Console.WriteLine(heap.ToString());
            heap.Poll();
            System.Console.WriteLine(heap.ToString());
            heap.Poll();
            System.Console.WriteLine(heap.ToString());
            heap.Poll();
            System.Console.WriteLine(heap.ToString());
        }
    }
}