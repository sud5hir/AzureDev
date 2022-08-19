// See https://aka.ms/new-console-template for more information
using Azure.Storage.Queues;

Console.WriteLine("Hello, World!");
TestQueue();

    static void TestQueue()
    {
        QueueClient queueClient = new QueueClient("DefaultEndpointsProtocol=https;AccountName=sudhirqueueaccount;AccountKey=O9aaOXlPyJmzo0BDzP0fp25mFG2k5ryF7InnHo0LM1/CJrVJdgwhU2O7EdqBsD0zb0il5Y/tGVPzlcErbaA4Qw==;EndpointSuffix=core.windows.net", "sudhirqueue");

        // Create the queue if it doesn't already exist
        queueClient.CreateIfNotExists();

        string message = "abc2";
        if (queueClient.Exists())
        {
            // Send a message to the queue
            queueClient.SendMessage(message);
        }

        Console.WriteLine($"Inserted: {message}");
    }
