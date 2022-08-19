// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Net;


// The container we will create.
Container container;

// The name of the database and container we will create
string databaseId = "FamilyDatabase";
string containerId = "FamilyContainer";


Console.WriteLine("Hello, World!");
const string conn = "https://sudhircosmodb.documents.azure.com:443/";
const string primaryKey = "IwqIiwDykeUbn648JqyOBAfCW9thfJa7Qe28eGZgKtd3QGU8RMyM7Ey40DerBMQP7pAfTgIXNLaugplCfPZNpw==";

CosmosClient cosmosClient = new CosmosClient(conn, primaryKey);
Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
container = await database.CreateContainerIfNotExistsAsync(containerId, "/LastName");

Family andersenFamily = new Family
{
    Id = "Andersen.1",
    LastName = "Andersen",
    Parents = new Parent[]
       {
            new Parent { FirstName = "Thomas" },
            new Parent { FirstName = "Mary Kay" }
       },
    Children = new Child[]
       {
            new Child
            {
                FirstName = "Henriette Thaulow",
                Gender = "female",
                Grade = 5,
                Pets = new Pet[]
                {
                    new Pet { GivenName = "Fluffy" }
                }
            }
       },
    Address = new Address { State = "WA", County = "King", City = "Seattle" },
    IsRegistered = false
};

try
{
    // Read the item to see if it exists.  
    ItemResponse<Family> andersenFamilyResponse = await container.ReadItemAsync<Family>(andersenFamily.Id, new PartitionKey(andersenFamily.LastName));
    Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource.Id);
}
catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
{
    // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
    ItemResponse<Family> andersenFamilyResponse = await container.CreateItemAsync<Family>(andersenFamily, new PartitionKey(andersenFamily.LastName));

    // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
    Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", andersenFamilyResponse.Resource.Id, andersenFamilyResponse.RequestCharge);
}

// Create a family object for the Wakefield family
Family wakefieldFamily = new Family
{
    Id = "Wakefield.7",
    LastName = "Wakefield",
    Parents = new Parent[]
    {
            new Parent { FamilyName = "Wakefield", FirstName = "Robin" },
            new Parent { FamilyName = "Miller", FirstName = "Ben" }
    },
    Children = new Child[]
    {
            new Child
            {
                FamilyName = "Merriam",
                FirstName = "Jesse",
                Gender = "female",
                Grade = 8,
                Pets = new Pet[]
                {
                    new Pet { GivenName = "Goofy" },
                    new Pet { GivenName = "Shadow" }
                }
            },
            new Child
            {
                FamilyName = "Miller",
                FirstName = "Lisa",
                Gender = "female",
                Grade = 1
            }
    },
    Address = new Address { State = "NY", County = "Manhattan", City = "NY" },
    IsRegistered = true
};

try
{
    // Read the item to see if it exists
    ItemResponse<Family> wakefieldFamilyResponse = await container.ReadItemAsync<Family>(wakefieldFamily.Id, new PartitionKey(wakefieldFamily.LastName));
    Console.WriteLine("Item in database with id: {0} already exists\n", wakefieldFamilyResponse.Resource.Id);
}
catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
{
    // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
    ItemResponse<Family> wakefieldFamilyResponse = await container.CreateItemAsync<Family>(wakefieldFamily, new PartitionKey(wakefieldFamily.LastName));

    // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
    Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", wakefieldFamilyResponse.Resource.Id, wakefieldFamilyResponse.RequestCharge);
}

var sqlQueryText = "SELECT * FROM c WHERE c.LastName = 'Andersen'";

Console.WriteLine("Running query: {0}\n", sqlQueryText);

QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
FeedIterator<Family> queryResultSetIterator = container.GetItemQueryIterator<Family>(queryDefinition);

List<Family> families = new List<Family>();

while (queryResultSetIterator.HasMoreResults)
{
    FeedResponse<Family> currentResultSet = await queryResultSetIterator.ReadNextAsync();
    foreach (Family family in currentResultSet)
    {
        families.Add(family);
        Console.WriteLine("\tRead {0}\n", family);
    }
}

 databaseId = "myfirstdb";
 containerId = "myfirstcontainer1";
Database database1 = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
Container container1 = await database1.CreateContainerIfNotExistsAsync(containerId, "/LastName");
//container = await database.CreateContainerIfNotExistsAsync(containerId, "/LastName");

Console.WriteLine("Running query: {0}\n", sqlQueryText);

sqlQueryText = "SELECT * FROM c WHERE c.name = 'nitu'";

Console.WriteLine("Running query: {0}\n", sqlQueryText);
queryDefinition = new QueryDefinition(sqlQueryText);
FeedIterator<object> queryResultSetIterator1 = container1.GetItemQueryIterator<object>(queryDefinition);



while (queryResultSetIterator1.HasMoreResults)
{
    FeedResponse<object> currentResultSet = await queryResultSetIterator1.ReadNextAsync();
    foreach (object family in currentResultSet)
    {
    
        Console.WriteLine("\tRead {0}\n", family);
    }
}
public class Family
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string LastName { get; set; }
    public Parent[] Parents { get; set; }
    public Child[] Children { get; set; }
    public Address Address { get; set; }
    public bool IsRegistered { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class Parent
{
    public string FamilyName { get; set; }
    public string FirstName { get; set; }
}

public class Child
{
    public string FamilyName { get; set; }
    public string FirstName { get; set; }
    public string Gender { get; set; }
    public int Grade { get; set; }
    public Pet[] Pets { get; set; }
}

public class Pet
{
    public string GivenName { get; set; }
}

public class Address
{
    public string State { get; set; }
    public string County { get; set; }
    public string City { get; set; }
}