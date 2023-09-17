using MediatR;
using n5now_api.Application.DTOs;

namespace n5now_api.Infrastructure.Queries
{
    public record GetPermissionByIdQuery(int Id) : IRequest<PermissionDto>;
}
