using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TodoApp.Integration.Tests.Helpers;
using TodoApp.WebApplication.Data;
using Xunit;

namespace TodoApp.Integration.Tests.IntegrationTests
{
    public class IndexPageTests : IClassFixture<CustomWebApplicationFactory<WebApplication.Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<WebApplication.Startup>
            _factory;

        public IndexPageTests(
            CustomWebApplicationFactory<WebApplication.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [Fact]
        public async Task Get_RedirectTo_TodoItems()
        {
            // Arrange
            // Act
            var defaultPage = await _client.GetAsync("/");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);


            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal("/TodoItems", content.Location.PathName);
        }

        [Fact]
        public async Task Get_ReturnsSuccess_WithTodoItems()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var serviceProvider = services.BuildServiceProvider();

                        using (var scope = serviceProvider.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices
                                .GetRequiredService<TodoAppContext>();
                            var logger = scopedServices
                                .GetRequiredService<ILogger<IndexPageTests>>();

                            try
                            {
                                Utilities.ReinitializeDbForTests(db);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "An error occurred seeding " +
                                                    "the database with test items. Error: {Message}",
                                    ex.Message);
                            }
                        }
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = true
                });

            // Act
            var defaultPage = await client.GetAsync("/");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Contains("Task 1", content.Body.TextContent);
            Assert.Contains("Task 2", content.Body.TextContent);
            Assert.Contains("Task 3", content.Body.TextContent);
            Assert.Contains("Task 4", content.Body.TextContent);

        }
    }
}
