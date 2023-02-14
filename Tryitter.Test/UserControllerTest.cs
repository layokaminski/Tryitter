using System.Net.Http.Json;
using Xunit;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Tryitter.Models;

namespace Tryitter.Test;

public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = new();

    public UserControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateStudentTest_Return_Created()
    {
        var student = new User
        {
            Name = "Usuário",
            Email = "user@teste.com",
            Password = "password",
        };


        HttpResponseMessage response = await _client.PostAsJsonAsync("/User", student);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }




}