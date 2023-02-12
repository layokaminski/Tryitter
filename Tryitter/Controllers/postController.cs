using Microsoft.AspNetCore.Mvc;
using Tryitter.Models;
using Tryitter.Repository;

[ApiController]
[Route("[controller]")]

public class PostController : ControllerBase
{
  private readonly IPostRepository _repository;
  public PostController(IPostRepository repository) 
  {
    _repository = repository;
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetPost(int id)
  {
    var post = await _repository.GetById(id);

    return Ok(post);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] Post post)
  {
    var postCreated = await _repository.Create(post);

    return CreatedAtAction("GetPost", new { id = postCreated.PostId }, postCreated);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update([FromBody] Post post, int id)
  {
    var updatePost = await _repository.Update(post, id);

    return Ok(updatePost);
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(int id)
  {
    _repository.Delete(id);

    return NoContent();
  }
}