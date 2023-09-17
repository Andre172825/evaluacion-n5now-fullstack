using MediatR;
using n5now_api.Application.DTOs;
using n5now_api.Infrastructure.Commands;
using n5now_api.Infrastructure.Queries;
using n5now_api.Infrastructure.Repositories;

namespace n5now_api.Application.Handlers
{
    public class GetAllPermissionsHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<PermissionDto>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetAllPermissionsHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionRepository.GetAllPermissions();
        }
    }
}
