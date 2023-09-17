using MediatR;
using n5now_api.Application.DTOs;
using n5now_api.Infrastructure.Queries;
using n5now_api.Infrastructure.Repositories;

namespace n5now_api.Application.Handlers
{

    public class GetPermissionByIdHandler : IRequestHandler<GetPermissionByIdQuery, PermissionDto>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionByIdHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<PermissionDto> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _permissionRepository.GetPermissionById(request.Id);
        }
    }
}
