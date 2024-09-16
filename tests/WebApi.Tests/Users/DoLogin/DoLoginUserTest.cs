using CashFlow.Communication.Requests.Users;
using CashFlow.Exception;
using Commom.Test.Utilities.Requests.Users;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Tests.InLineData;

namespace WebApi.Tests.Users.DoLogin;
public class DoLoginUserTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string PATH_URI = "api/login";

    private readonly HttpClient _httpClient;

    public DoLoginUserTest(CustomWebApplicationFactory webApplicationFactory)
    {
        webApplicationFactory.Server.PreserveExecutionContext = true;
        _httpClient = webApplicationFactory.CreateClient();        
    }

    [Fact]
    public async Task Success()
    {
        var user = RequestRegisterUserJsonBuilder.Build();

        await _httpClient.PostAsJsonAsync("api/users", user);

        var request = new RequestLoginUserJson
        {
            Email = user.Email,
            Password = user.Password
        };

        var result = await _httpClient.PostAsJsonAsync(PATH_URI, request);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = result.Content.ReadAsStream();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(user.Name);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();   
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Invalid_Login(string cultureInfo)
    {
        var user = RequestLoginUserJsonBuilder.Build();

        var result = await _httpClient.PostAsJsonAsync(PATH_URI, user);

        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("INVALID_CREDENTIALS", new CultureInfo(cultureInfo));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
