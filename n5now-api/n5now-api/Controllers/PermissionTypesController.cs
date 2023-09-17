using MediatR;
using Microsoft.AspNetCore.Mvc;
using n5now_api.Application.DTOs;
using n5now_api.Infrastructure.Queries;

namespace n5now_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PermissionTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionTypeDto>>> GetAll()
        {
            var query = new GetAllPermissionTypesQuery();
            var permissionTypes = await _mediator.Send(query);
            if (!permissionTypes.Any())
            {
                return NoContent();
            }
            return Ok(permissionTypes);
        }
    }
}
