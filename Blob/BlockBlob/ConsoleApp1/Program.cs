// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage.Blob;

Console.WriteLine("Azure Blob Storage v12 - .NET quickstart sample\n");

BlobServiceClient blobServiceClient =
    new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=sudhirblobaccount;AccountKey=6LjqUAskaNJyFmYH9n23qynAtkq9l0H3T5ZYbGfUNVeUjhdprR/2ZJmrVNAK9CZNdiLAfXsvD9QyXqXKYEZ99g==;EndpointSuffix=core.windows.net");

BlobContainerClient containerClient1 =  blobServiceClient.GetBlobContainerClient("sudhircontainer");

////Create a unique name for the container
string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

//// Create the container and return a container client object
BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

//// Create a local file in the ./data/ directory for uploading and downloading
string localPath = "C:/data/";
string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
string localFilePath = Path.Combine(localPath, fileName);

//// Write text to the file
await File.WriteAllTextAsync(localFilePath, "Hello, World!");

// Get a reference to a blob
BlobClient blobClient = containerClient.GetBlobClient(fileName);

Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

// Upload data from the local file
await blobClient.UploadAsync(localFilePath);

Console.WriteLine("Listing blobs...");

// List all blobs in the container
await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
{
    Console.WriteLine("\t" + blobItem.Name);
}

string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

// Download the blob's contents and save it to a file
await blobClient.DownloadToAsync(downloadFilePath);

Console.ReadLine();

Console.ReadLine();

Console.WriteLine("Deleting blob container...");
await containerClient.DeleteAsync();

Console.WriteLine("Deleting the local source and downloaded files...");
File.Delete(localFilePath);
File.Delete(downloadFilePath);

Console.WriteLine("Done");

//CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

//CloudBlobContainer cloudBlobContainer =
//    cloudBlobClient.GetContainerReference("quickstartblobs" +
//        Guid.NewGuid().ToString());
//// Get a reference to the blob address, then upload the file to the blob.
//// Use the value of localFileName for the blob name.
//CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
//cloudBlockBlob.op
//await cloudBlockBlob.UploadFromFileAsync(sourceFile);