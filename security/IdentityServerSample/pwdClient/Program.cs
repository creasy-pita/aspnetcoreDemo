using System;
using System.Net.Http;
using IdentityModel.Client;


namespace PwdClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var diso = DiscoveryClient.GetAsync("http://localhost:5000").Result;
            if(diso.IsError)
            {
                Console.WriteLine(diso.Error);
            }
            // var tokenClient = new TokenClient(diso.TokenEndpoint,"client","secret");
            // var tokenResponse = tokenClient.RequestClientCredentialsAsync("api").Result;
            var tokenClient = new TokenClient(diso.TokenEndpoint,"pwdClient","secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("jesse","123456","api").Result;            
            if(tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var httpResponse = httpClient.GetAsync("http://localhost:5001/api/values").Result;
            if(httpResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(httpResponse.Content.ReadAsStringAsync().Result);
            }
            Console.ReadLine();
        }
    }
}
