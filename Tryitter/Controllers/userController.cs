using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly ITryRepository _repository;
  public UserController(ITryRepository repository) 
  {
    _repository = repository;
  }

  [HttpGet]
  public async Task<IActionResult> GetUser()
  {
    throw new NotImplementedException();
  }

  [HttpPost]
  public async Task<IActionResult> Create(User user)
  {
    throw new NotImplementedException();
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