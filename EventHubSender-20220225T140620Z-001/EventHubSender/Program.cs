using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventHubSender
{
    class Program
    {
        // connection string to the Event Hubs namespace
        private const string connectionString = "Endpoint=sb://publisherebenthib.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=qzl8zzOrw6xGaGlbTc3dwVIB4y/fAn7anztiCdGhfY0=";
        
        // name of the event hub
        private const string eventHubName = "testpubevent";

        // number of events to be sent to the event hub
        private const int numOfEvents = 3;

        static EventHubProducerClient producerClient;
        static async Task Main(string[] args)
        {
            // Create a producer client that you can use to send events to an event hub
            producerClient = new EventHubProducerClient(connectionString, eventHubName);
            
            var partitionsIds=producerClient.GetPartitionIdsAsync();

            // Create a batch of events 
            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

            for (int i = 1; i <= numOfEvents; i++)
            {
                if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"{@"C:\\Users\\sud5h\\OneDrive\\Pictures\\Screenshots\S\creenshot (6).png"}"))))
                {
                    // if it is too large for the batch
                    throw new Exception($"Event {i} is too large for the batch and cannot be sent.");
                }
            }

            try
            {
                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine($"A batch of {numOfEvents} events has been published.");
            }
            finally
            {
                await producerClient.DisposeAsync();
            }
        }
    }
}
