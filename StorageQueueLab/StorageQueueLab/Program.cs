using Azure.Storage.Queues;
using System;
using System.Configuration;

namespace StorageQueueLab
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from app settings
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountrgla9570;AccountKey=vnW13gvwd9MEREF3r0Sxpdn9sFJb3oWzfM140ZWH9J9eF14zoh5kRm7UM0f5OgjI+iGKxqwGCTcgFdgW9RokLw==;EndpointSuffix=core.windows.net";

            string queueName = "sudhirstoragequeue";
            string message = "Hello world";
            // Instantiate a QueueClient which will be used to create and manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, queueName);
            // Create the queue if it doesn't already exist
            queueClient.CreateIfNotExists();
            if (queueClient.Exists())
            {
                // Send a message to the queue
                queueClient.SendMessage(message);
                int batchSize = 10;
                TimeSpan visibilityTimeout = TimeSpan.FromSeconds(2.5d);
               // Response<QueueMessage[]> messages = await client.ReceiveMessagesAsync(batchSize, visibilityTimeout);

                var msg = queueClient.ReceiveMessage();
                Console.WriteLine(msg.Value.Body.ToString());
            }
            Console.ReadLine();
        }
    }
}
