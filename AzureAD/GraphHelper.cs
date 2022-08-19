using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using TimeZoneConverter;

namespace AzureAD
{
    internal class GraphHelper
    {
        private static  DeviceCodeCredential _deviceCodeCredential;
        private static GraphServiceClient _graphServiceClient;

        public static void Initialize(string clientId,string [] scopes,
            Func<DeviceCodeInfo,CancellationToken,Task> callback)
        {
            _deviceCodeCredential=new DeviceCodeCredential(callback,clientId);
            _graphServiceClient=new GraphServiceClient(_deviceCodeCredential,scopes);
        }

        public static async Task<string> GetAccessTokenAsync(string[] scopes)
        {
            var context= new TokenRequestContext(scopes);
            var response = await _deviceCodeCredential.GetTokenAsync(context);
            return response.Token;
        }
    }
}
