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

    if (user == null)
    {
      return NoContent();
    }

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
  [Authorize]
  public async Task<IActionResult> UpdateUser([FromBody] User user, int id)
  {

    var getUser = await _repository.GetUserById(id);
    
    if (getUser == null)
    {
      return BadRequest("Algo deu errado!");
    }

    var updateUser = await _repository.UpdateUser(user, id);
    
    return Ok(updateUser);
  }

  [HttpDelete]
  [Authorize]
  [Route("{id}")]
  public async Task<IActionResult> Delete(int id)
  {

     var getUser = await _repository.GetUserById(id);
    
    if (getUser == null)
    {
      return BadRequest("Algo deu errado!");
    }
    
    _repository.DeleteUser(id);

    return NoContent();
  }
}