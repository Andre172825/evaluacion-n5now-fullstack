using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n5now_api.Application.DTOs;
using n5now_api.Domain.Models;
using n5now_api.Infrastructure.Commands;
using n5now_api.Infrastructure.Queries;
using n5now_api.Infrastructure.Repositories;
using Nest;
using System.Security;

namespace n5now_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ElasticClient _client;
        public PermissionsController(IMediator mediator, IESClientProvider clientProvider)
        {
            _mediator = mediator;
            _client = clientProvider.GetClient();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDto>> GetById(int id)
        {
            var query = new GetPermissionByIdQuery(id);
            var permission = await _mediator.Send(query);
            if(permission == null)
            {
                return NotFound();
            }

            return Ok(permission);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetAll()
        {
            var query = new GetAllPermissionsQuery();
            var permissions = await _mediator.Send(query);
            if (!permissions.Any())
            {
                return NoContent();
            }
            return Ok(permissions);
        }

        [HttpPost]
        public async Task<ActionResult<PermissionDto>> Create(CreatePermissionCommand command)
        {
            var permission = await _mediator.Send(command);
            if (permission == null)
            {
                return BadRequest();
            }
            _client.Index(permission, idx => idx.Id(permission.Id));//guardar en elastic search
            return Ok(permission);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PermissionDto>> Update(int id, UpdatePermissionCommand command)
        {
            if(id != command.Id)
            {
                return BadRequest();
            }
            var permission = await _mediator.Send(command);
            if(permission == null)
            {
                return NotFound();
            }
            await _client.DeleteAsync<Permission>(permission.Id.ToString());
            _client.Index(permission, idx => idx.Id(permission.Id));
            return Ok(permission);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeletePermissionCommand(id));
            if (result == 0)
            {
                return NotFound(false);
            }
            await _client.DeleteAsync<Permission>(result.ToString());
            return Ok(true);
        }
    }
}
