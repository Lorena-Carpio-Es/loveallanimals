using System;
using Microsoft.AspNetCore.Mvc;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Dto;

namespace Love4AnimalsAPI.Controllers;

[ApiController]
[Route("v1/post/{postId}/comment")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _service;
    private readonly IPostService _postService;

    public CommentController(ICommentService service, IPostService postService)
    {
        _service = service;
        _postService = postService;
    }

    // GET comentarios de un post
    [HttpGet]
    public IActionResult GetByPost(long postId)
    {
        return Ok(_service.GetByPost(postId));
    }

    // POST crear comentario
    [HttpPost]
    public IActionResult Create(long postId, [FromBody] CreateCommentDto dto)
    {
        var post = _postService.GetById(postId);

        if (post == null)
            return NotFound();

        var comment = new Comment
        {
            Text = dto.Text
        };

        var created = _service.Create(postId, comment);
        return CreatedAtAction(nameof(GetByPost), new { postId = postId }, created);
    }

    // PUT actualizar comentario
    [HttpPut("{id}")]
    public IActionResult Update(long id, [FromBody] UpdateCommentDto dto)
    {
        var comment = new Comment
        {
            Text = dto.Text
        };

        var updated = _service.Update(id, comment);
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    // DELETE comentario
    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        if (!_service.Delete(id)) return NotFound();

        return NoContent();
    }
}