using System.Net.Http.Json;
using Xunit;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Tryitter.Models;
using System.Collections.ObjectModel;
using Tryitter.Repository;

namespace Tryitter.Test;

public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = new();

    public UserControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Should_Create_Student()
    {
        var student = new User
        {
            Name = "Usuário",
            Email = "user@teste.com",
            Password = "password",
            Posts = new Collection<Post>(),
        };


        HttpResponseMessage response = await _client.PostAsJsonAsync("/User", student);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Should_Not_Create_Student()
    {
        var student = new User();


        HttpResponseMessage response = await _client.PostAsJsonAsync("/User", student);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Get_Student()
    {
        Random rnd = new();
        int num = rnd.Next(99);

        var student = new User
        {
            UserID = num,
            Name = "Usuário",
            Email = "user@teste.com",
            Password = "password",
            Posts = new Collection<Post>(),
        };

        await _client.PostAsJsonAsync("/User", student);

        
        HttpResponseMessage response = await _client.GetAsync($"User/{num}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact]
    public async Task Should_Not_Get_Student()
    {
       

        HttpResponseMessage response = await _client.GetAsync("User/1000");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

    }
}