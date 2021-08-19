using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TodoApp.Integration.Tests.IntegrationTests
{
    public class BasicTests : IClassFixture<WebApplicationFactory<WebApplication.Startup>>
    {
        private readonly WebApplicationFactory<WebApplication.Startup> _factory;

        public BasicTests(WebApplicationFactory<WebApplication.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/TodoItems")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
