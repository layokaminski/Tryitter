using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tryitter.Models;
using Tryitter.Token;
using Tryitter.Repository;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserRepository _repository;
  public UserController(IUserRepository repository) 
  {
    _repository = repository;
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetUserById(int id)
  {
    var user = await _repository.GetUserById(id);

    return Ok(user);
  }

  [HttpPost]
  public async Task<IActionResult> CreateUser([FromBody] User user)
  {
    var userCreated = await _repository.CreateUser(user);

    if (userCreated == null)
    {
      return BadRequest("Algo deu errado!");
    }
    
    var token = new TokenGenerator().Generate(userCreated);

    return Created("GetUser", new { token });
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateUser([FromBody] User user, int id)
  {
    var updateUser = await _repository.UpdateUser(user, id);

    return Ok(updateUser);
  }

  [HttpDelete]
  [Authorize(Policy = "login")]
  [Route("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    _repository.DeleteUser(id);

    return NoContent();
  }
}