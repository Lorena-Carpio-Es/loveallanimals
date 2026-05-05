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

    // 🔵 GET comentarios por post
    [HttpGet]
    public async Task<IActionResult> GetByPost(long postId)
    {
        return Ok(await _service.GetByPostAsync(postId));
    }

    // 🟢 POST crear comentario
    [HttpPost]
    public async Task<IActionResult> Create(long postId, CreateCommentDto dto)
    {
        // 🔥 Validar que el post exista (PRO nivel defensa)
        var post = await _postService.GetByIdAsync(postId);
        if (post == null)
            return BadRequest("El post no existe ❌");

        var comment = new Comment
        {
            Text = dto.Text,
            PostId = postId
        };

        return Ok(await _service.CreateAsync(comment));
    }

    // 🟡 PUT actualizar comentario
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

    // 🔴 DELETE comentario
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}