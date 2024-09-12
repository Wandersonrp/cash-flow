using Commom.Test.Utilities.Requests.Users;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace WebApi.Tests.Users.Register;
public class RegisterUserTests : IClassFixture<CustomWebApplicationFactory>
{
    private const string PATH_URI = "api/users";

    private readonly HttpClient _httpClient;

    public RegisterUserTests(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();        

        var response = await _httpClient.PostAsJsonAsync(PATH_URI, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
