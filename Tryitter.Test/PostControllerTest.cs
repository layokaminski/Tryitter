using System.Net.Http.Json;
using Xunit;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Tryitter.Models;
using System.Collections.ObjectModel;
using Tryitter.Token;
using System.Net.Http.Headers;
using Moq;
using Tryitter.Repository;

namespace Tryitter.Test;

public class PostControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = new();

    private readonly Mock<IPostRepository> mock = new();

    public PostControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Should_Create_Post()
    {
        var student = new User
        {
            Name = "Usuário",
            Email = "user@teste.com",
            Password = "password",
            Posts = new Collection<Post>(),
        };


        await _client.PostAsJsonAsync("/User", student);

        var token = new TokenGenerator().Generate(student);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var post = new Post
        {
            Description = "Postado com sucesso!",
            UserId = 1,
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync("/Post", post);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Should_Get_Post()
    {
       var student = new User
        {
            Name = "Usuário",
            Email = "user@teste.com",
            Password = "password",
            Posts = new Collection<Post>(),
        };


        await _client.PostAsJsonAsync("/User", student);

        var token = new TokenGenerator().Generate(student);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var post = new Post
        {
            Description = "Postado com sucesso!",
            UserId = 1,
        };

        var posted = await _client.PostAsJsonAsync("/Post", post).Result.Content.ReadFromJsonAsync<Post>();
    
        HttpResponseMessage response = await _client.GetAsync($"/Post/{posted.PostId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Not_Get_Post()
    {
       
        HttpResponseMessage response = await _client.GetAsync("/Post/1000");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task Should_Edit_Post()
    {
        var student = new User
        {
        Name = "Usuário",
        Email = "user@teste.com",
        Password = "password",
        Posts = new Collection<Post>(),
        };

        await _client.PostAsJsonAsync("/User", student);

        var token = new TokenGenerator().Generate(student);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var post = new Post
        {
        Description = "Postado com sucesso!",
        UserId = 1,
        };


        var posted = await _client.PostAsJsonAsync("/Post", post).Result.Content.ReadFromJsonAsync<Post>();

        post.Description = "Editado com sucesso !";

        HttpResponseMessage response = await _client.PutAsJsonAsync($"/Post/{posted.PostId}", post);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Delete_Post()
    {
        var student = new User
        {
            Name = "Usuário",
            Email = "user@teste.com",
            Password = "password",
            Posts = new Collection<Post>(),
        };


        await _client.PostAsJsonAsync("/User", student);

        var token = new TokenGenerator().Generate(student);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        Random rnd = new();
        int num = rnd.Next(99);

        var post = new Post
        {
            PostId = num,
            Description = "Postado com sucesso!",
            UserId = 1,
        };

        await _client.PostAsJsonAsync("/Post", post);

        HttpResponseMessage response = await _client.DeleteAsync($"/Post/{num}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    

}