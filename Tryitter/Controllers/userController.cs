using Microsoft.AspNetCore.Mvc;
using Tryitter.Models;
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

    return CreatedAtAction("GetUser", new { id = student.UserID }, student);
  }

  [HttpPut]
  [Route("{id}")]
  public async Task<IActionResult> Put(int id)
  {
    throw new NotImplementedException();
  }

  [HttpDelete]
  [Route("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    throw new NotImplementedException();
  }
}