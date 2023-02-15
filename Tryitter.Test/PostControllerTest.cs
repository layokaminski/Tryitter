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
    public async Task Should_Not_Create_Post()
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

        var post = new Post();


        HttpResponseMessage response = await _client.PostAsJsonAsync("/Post", post);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Token_Required()
    {
        var student = new User
        {
            Name = "Usuário",
            Email = "user@teste.com",
            Password = "password",
            Posts = new Collection<Post>(),
        };


        await _client.PostAsJsonAsync("/User", student);


        var post = new Post
        {
            Description = "Postado com sucesso!",
            UserId = 1,
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync("/Post", post);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
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
       
        HttpResponseMessage response = await _client.GetAsync("/Post/0");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_Get_Posts()
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

    
        HttpResponseMessage response = await _client.GetAsync("/Post");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
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
         Random rnd = new();

         int userId = rnd.Next(99);

        var student = new User
        {
            UserID = userId,
            Name = "Usuário",
            Email = "user@teste.com",
            Password = "password",
            Posts = new Collection<Post>(),
        };


        await _client.PostAsJsonAsync("/User", student);

        var token = new TokenGenerator().Generate(student);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

       
        int postId = rnd.Next(99);

        var post = new Post
        {
            PostId = postId,
            Description = "Postado com sucesso!",
            UserId = userId,
        };

        await _client.PostAsJsonAsync("/Post", post);

        HttpResponseMessage response = await _client.DeleteAsync($"/Post/{postId}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_Not_Delete_Post()
    {
         Random rnd = new();

         int userId = rnd.Next(99);

        var student = new User
        {
            UserID = userId,
            Name = "Usuário",
            Email = "user@teste.com",
            Password = "password",
            Posts = new Collection<Post>(),
        };


        await _client.PostAsJsonAsync("/User", student);

        var token = new TokenGenerator().Generate(student);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        HttpResponseMessage response = await _client.DeleteAsync("/Post/0");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Get_Posts_By_User()
    {
        Random rnd = new();

         int userId = rnd.Next(99);

        var student = new User
        {
            UserID = userId,
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

        var post2 = new Post
        {
            Description = "Postado com sucesso  2!",
            UserId = 1,
        };

        await _client.PostAsJsonAsync("/Post", post);
        await _client.PostAsJsonAsync("/Post", post2);
    
        HttpResponseMessage response = await _client.GetAsync($"/Post/user/{userId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    

}