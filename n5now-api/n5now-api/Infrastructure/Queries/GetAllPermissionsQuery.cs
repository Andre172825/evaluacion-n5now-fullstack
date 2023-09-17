using MediatR;
using n5now_api.Application.DTOs;

namespace n5now_api.Infrastructure.Queries
{
    public record GetAllPermissionsQuery : IRequest<IEnumerable<PermissionDto>>;
}
