using Microsoft.AspNetCore.Mvc;
using Tryitter.Models;
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

    return CreatedAtAction("GetUser", new { id = userCreated.UserID }, userCreated);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateUser([FromBody] User user, int id)
  {
    var updateUser = await _repository.UpdateUser(user, id);

    return Ok(updateUser);
  }

  [HttpDelete("{id}")]
  public IActionResult DeleteUser(int id)
  {
    _repository.DeleteUser(id);

    return NoContent();
  }
}