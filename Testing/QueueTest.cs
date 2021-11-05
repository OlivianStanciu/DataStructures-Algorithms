using System;
using C_InANutShell.Queues;

namespace C_InANutShell.Testing
{
    public class QueueTest : ITest
    {
        public void Execute()
        {
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(4);

            queue.Dequeue();
            queue.Enqueue(5);

            System.Console.WriteLine(queue.ToString());
        }
    }
}