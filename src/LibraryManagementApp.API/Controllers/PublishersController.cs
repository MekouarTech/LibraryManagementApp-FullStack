using Microsoft.AspNetCore.Mvc;
using MediatR;
using LibraryManagementApp.Application.Publishers.Commands.CreatePublisher;
using LibraryManagementApp.Application.Publishers.Commands.UpdatePublisher;
using LibraryManagementApp.Application.Publishers.Commands.DeletePublisher;
using LibraryManagementApp.Application.Publishers.Queries.GetAllPublishers;
using LibraryManagementApp.Application.Publishers.Queries.GetPublisherById;
using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PublishersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PublisherDto>>> GetAll()
    {
        var query = new GetAllPublishersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PublisherDto>> GetById(int id)
    {
        var query = new GetPublisherByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PublisherDto>> Create([FromBody] CreatePublisherCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PublisherDto>> Update(int id, [FromBody] UpdatePublisherCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID in URL does not match ID in request body");
        }

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeletePublisherCommand { Id = id };
        var result = await _mediator.Send(command);
        
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
} 