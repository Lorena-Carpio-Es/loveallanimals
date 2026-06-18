using System;
using Microsoft.AspNetCore.Mvc;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Dto;
using Love4AnimalsAPI.Repositories;

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

   
    [HttpGet]
public async Task<IActionResult> GetByPost(long postId)
{
    var comments = await _service.GetByPostAsync(postId);

    var response = comments.Select(c => new CommentResponseDto
    {
        Id = c.Id,
        Text = c.Text,
        Date = c.Date,
        PostId = c.PostId
    });

    return Ok(response);
}

[HttpPost]
public async Task<IActionResult> Create(long postId, CreateCommentDto dto)
{
    var post = await _postService.GetByIdAsync(postId);

    if (post == null)
        return BadRequest("El post no existe");

    var comment = new Comment
    {
        Text = dto.Text,
        PostId = postId
    };

    var created = await _service.CreateAsync(comment);

    var response = new CommentResponseDto
    {
        Id = created.Id,
        Text = created.Text,
        Date = created.Date,
        PostId = created.PostId
    };

    return Ok(response);
}

   
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateCommentDto dto)
    {
        var comment = new Comment
        {
            Text = dto.Text
        };

        var ok = await _service.UpdateAsync(id, comment);
        return ok ? NoContent() : NotFound();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}