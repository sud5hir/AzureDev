
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=sudhirstorageaccount1;AccountKey=wQkh4IWT016p56StNCKlRKRU/GGySO/mS51Ns/T2HaWgvFrxRtKL3S12PWsHRR1QYLN+PNxbN2NJ+AStmufeCA==;EndpointSuffix=core.windows.net");

var client = account.CreateCloudTableClient();
var table = client.GetTableReference("sudhirtable");

table.CreateIfNotExistsAsync();

EmployeeEntity employeeEntity = new EmployeeEntity("Orders", "6")
{
    PatientName = "m",
    OrderId = "0"

};

TableOperation insertOperation = TableOperation.Retrieve<EmployeeEntity>("Orders","1");
TableResult t= await table.ExecuteAsync(insertOperation);

TableOperation insertOperation1 = TableOperation.Insert(employeeEntity);
TableResult t1 = await table.ExecuteAsync(insertOperation1);

var condition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Sbeeh");
var query1 = new TableQuery<EmployeeEntity>().Where(condition);

//var lst = table.ExecuteAsync(query1);

public class EmployeeEntity : TableEntity
{
    public EmployeeEntity(string lastName, string firstName)
    {
        this.PartitionKey = lastName; this.RowKey = firstName;
    }
    public EmployeeEntity() { }
    public string OrderId { get; set; }
    public string PatientName { get; set; }
}