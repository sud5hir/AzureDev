// See https://aka.ms/new-console-template for more information
using Azure;
using Azure.Messaging.EventGrid;

    //private const string topicEndpoint = "https://queryapi61.centralindia-1.eventgrid.azure.net/api/events";
    //private const string topicKey = "78GtIftYgg+2z+JLvSrux4SUSa4TPRFtcRisSoUZJ6Q=";

    Console.WriteLine("Hello, World!");


    Uri endpoint = new Uri("https://logicapptopic.eastus-1.eventgrid.azure.net/api/events");
    AzureKeyCredential credential = new AzureKeyCredential("P49ZJOxbL0MfUb35tVrpiKgWa+bOU0f09tVi1kEZWI0=");
    EventGridPublisherClient client = new EventGridPublisherClient(endpoint, credential);

    EventGridEvent firstEvent = new EventGridEvent(
        subject: $"New Employee: Alba Sutton",
        eventType: "Employees.Registration.New",
        dataVersion: "1.0",
        data: new
        {
            FullName = "Alba Sutton",
            Address = "4567 Pine Avenue, Edison, WA 97202"
        }
    );

    EventGridEvent secondEvent = new EventGridEvent(
        subject: $"New Employee: Alexandre Doyon",
        eventType: "Employees.Registration.New",
        dataVersion: "1.0",
        data: new
        {
            FullName = "Alexandre Doyon",
            Address = "456 College Street, Bow, WA 98107"
        }
    );

    await client.SendEventAsync(firstEvent);
    Console.WriteLine("First event published");

    await client.SendEventAsync(secondEvent);
    Console.WriteLine("Second event published");