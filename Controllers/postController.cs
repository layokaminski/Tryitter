using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class PostController : ControllerBase
{
  public PostController() {}

  [HttpGet]
  public async Task<IActionResult> GetPost()
  {
    throw new NotImplementedException();
  }

  [HttpPost]
  public async Task<IActionResult> Create(Post Post)
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