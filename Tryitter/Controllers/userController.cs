using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tryitter.Models;
using Tryitter.Token;
using Tryitter.Repository;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly ITryRepository _repository;
  public UserController(ITryRepository repository) 
  {
    _repository = repository;
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetUser(int id)
  {
    var student = await _repository.GetById<User>(id);

    return Ok(student);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] User user)
  {
    var student = await _repository.Create(user);

    if (student == null)
    {
      return BadRequest("Algo deu errado!");
    }
    
    var token = new TokenGenerator().Generate(student);

    return CreatedAtAction("GetUser", new { id = student.UserID }, token);

    // return CreatedAtAction("GetUser", new { id = student.UserID }, student);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Put([FromBody] User user, int id)
  {
    throw new NotImplementedException();
  }

  [HttpDelete]
  [Authorize(Policy = "login")]
  [Route("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    throw new NotImplementedException();
  }
}