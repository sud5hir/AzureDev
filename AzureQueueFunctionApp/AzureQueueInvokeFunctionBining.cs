using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AzureQueueFunctionApp
{
    public class AzureQueueInvokeFunctionBining
    {
        [FunctionName("queueTrigger")]
        public void Run([QueueTrigger("inputqueue", Connection = "queueappstorageaccountconnectionstring")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }     
    }
}

//Send to output queue from function app

//using System.Net;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Primitives;
//using Newtonsoft.Json;

//public static async Task<IActionResult> Run(HttpRequest req, ICollector<string> outputSbMsg, ILogger log)
//{
//    log.LogInformation("C# HTTP trigger function processed a request.");

//    string name = req.Query["name"];

//    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//    dynamic data = JsonConvert.DeserializeObject(requestBody);
//    name = name ?? data?.name;
//    outputSbMsg.Add("Name passed to the function: " + name);
//    log.LogInformation($"C# Queue trigger function processed: {outputSbMsg}");
//    string responseMessage = string.IsNullOrEmpty(name)
//        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
//                : $"Hello, {name}. This HTTP triggered function executed successfully.";

//    return new OkObjectResult(responseMessage);
//}
