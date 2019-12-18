using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using API.Test.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace API.Test.Controllers
{
    public class ExampleControllerTest
    {

        private readonly HttpClient _client;
        private string apiPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, "src\\API\\");


        public ExampleControllerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(apiPath)
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(apiPath)
                    .AddJsonFile("appsettings.json")
                    .Build())
                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [Fact]
        public async Task GetAll_WithoutAuthorization_Returns401()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.GetAsync(ApiRoutes.Example.GetAll);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task Authenticate_WhenCorrectLogin_ShouldReturnToken()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;
            var login = new AuthenticateExample { Username = "test", Password = "test" };
            var request = new RequestHelper<AuthenticateExample>(login).Data;

            // Act
            var response = await _client.PostAsync(ApiRoutes.Example.Authenticate, request);
            var data = response.Data().ToUserToken();

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            data.Token.Should().NotBeNullOrEmpty();
        }
    }
}
