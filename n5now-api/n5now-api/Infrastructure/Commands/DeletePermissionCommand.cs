using MediatR;

namespace n5now_api.Infrastructure.Commands
{
    public record DeletePermissionCommand(int Id) : IRequest<int>;
}
