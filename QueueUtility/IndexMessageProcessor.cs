using System;
using System.Threading;

namespace BHL.QueueUtility
{
    public class IndexMessageProcessor : IMessageProcessor
    {
        public bool ProcessMessage(string message)
        {
            Console.WriteLine("Read message from queue: {0}", message);

            // Index the item in a search server
            Thread.Sleep(5000); // Include this for demo purposes

            Console.WriteLine("Completed processing message: {0}", message);

            return true;
        }
    }
}
