using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace MessageProcessor
{
    class Program
    {
        private const string connectionString = "Endpoint=sb://sbnamespace12.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=37lJ3CaBWNRs3DtSHh524V96xl0H2IEvfBl2oHj+zEc=";
        private const string topiceorqueueName = "dtt";
        static ServiceBusClient client;
        static ServiceBusSender sender;
        private const int numOfMessages = 3;

        public static async Task Main(string[] args)
        {
            client = new ServiceBusClient(connectionString);
            sender = client.CreateSender(topiceorqueueName);

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            for (int i = 1; i <= numOfMessages; i++)
            {
                //if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
                //{
                //    throw new Exception($"The message {i} is too large to fit in the batch.");
                //}


                if (i == 1)
                {
                    ServiceBusMessage message = new() { Subject = "messageBatch1" };
                    message.ApplicationProperties.Add("Color", $"Message {i}");
                    message.CorrelationId = "s2";
                    messageBatch.TryAddMessage(message);
                }
                else
                {
                    ServiceBusMessage message = new() { Subject = "messageBatch2" };
                    message.ApplicationProperties.Add("Color", $"Message {i}");
                    message.CorrelationId = "sudhifilter";
                    messageBatch.TryAddMessage(message);
                }              

            }         
         

            //List<ServiceBusMessage> messages = new();
            //messages.Add(CreateMessage(subject: "Red"));
            //messages.Add(CreateMessage(subject: "Blue"));
            //messages.Add(CreateMessage(subject: "Red", correlationId: "important"));
            //messages.Add(CreateMessage(subject: "Blue", correlationId: "important"));
            //messages.Add(CreateMessage(subject: "Red", correlationId: "notimportant"));
            //messages.Add(CreateMessage(subject: "Blue", correlationId: "notimportant"));
            //messages.Add(CreateMessage(subject: "Green"));
            //messages.Add(CreateMessage(subject: "Green", correlationId: "important"));
            //messages.Add(CreateMessage(subject: "Green", correlationId: "notimportant"));

            try
            {
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}