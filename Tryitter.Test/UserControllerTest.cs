using System.Net.Http.Json;
using Xunit;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Tryitter.Models;
using System.Collections.ObjectModel;
using Tryitter.Repository;
using System.Net.Http.Headers;
using Tryitter.Token;

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
       

        HttpResponseMessage response = await _client.GetAsync("User/0");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

    }

    [Fact]
    public async Task Should_Edit_Student()
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

        student.Name = "Novo nome";

        
        HttpResponseMessage response = await _client.PutAsJsonAsync($"User/{num}", student);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact]
    public async Task Should_Not_Edit_Student()
    {
        var student = new User
        {
            UserID = 0,
            Name = "Usuário",
            Email = "user@teste.com",
            Password = "password",
            Posts = new Collection<Post>(),
        };
       

        HttpResponseMessage response = await _client.PutAsJsonAsync("User/o", student);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Should_Delete_Student()
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

        var token = new TokenGenerator().Generate(student);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await _client.DeleteAsync($"User/{num}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

    }

    [Fact]
    public async Task Should_Not_Delete_Student()
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

        var token = new TokenGenerator().Generate(student);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await _client.DeleteAsync("User/0");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Token_Required()
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


        HttpResponseMessage response = await _client.DeleteAsync($"User/{num}");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }


}