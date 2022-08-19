using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;

namespace KeyValutBlobFa
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string blobstorageconnection = Environment.GetEnvironmentVariable("sudhir204saconn");

            CloudBlockBlob blockBlob;
            await using (MemoryStream memoryStream = new MemoryStream())
            {
             //   string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("sudhirblob");
                blockBlob = cloudBlobContainer.GetBlockBlobReference("records.json");
                await blockBlob.DownloadToStreamAsync(memoryStream);
            }
            Stream blobStream = blockBlob.OpenReadAsync().Result;

            CloudStorageAccount cloudStorageAccount1 = CloudStorageAccount.Parse(blobstorageconnection);
            CloudBlobClient cloudBlobClient1 = cloudStorageAccount1.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer1 = cloudBlobClient1.GetContainerReference("sudhirblob");

            CloudBlockBlob blockBlob1 = cloudBlobContainer1.GetBlockBlobReference(@"downloadedrecords.json");
          //  await using (var data = files.OpenReadStream())
          
                await blockBlob1.UploadFromStreamAsync(blobStream);
            //}


            // var sw=new StreamWriter(blobStream);
            // sw.WriteLine(@"D:\Sudhir_Resume\Sudhir_learn\Azure\Az 204-siddhesh");
            return new OkObjectResult(blobstorageconnection+"sudhir4" + blobStream);
            //log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //return new OkObjectResult(responseMessage);
        }
    }
}
