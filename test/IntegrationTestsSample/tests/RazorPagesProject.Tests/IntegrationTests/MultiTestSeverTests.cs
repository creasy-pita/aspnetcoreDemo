using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RazorPagesProject.Tests.IntegrationTests
{
    public class MultiTestSeverTests
    {
        private readonly TestServer _identityTestServer;
        private readonly TestServer _mgmtTestServer;

        public MultiTestSeverTests() 
        {
            _mgmtTestServer = new TestServer(new WebHostBuilder()
                .UseStartup<RazorPagesProject.Startup>()
                .UseEnvironment("IntegrationTest"));
        }

        [Theory]
        [InlineData("/test/index")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _mgmtTestServer.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            //Console.WriteLine(response.Headers.ToString());

            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
