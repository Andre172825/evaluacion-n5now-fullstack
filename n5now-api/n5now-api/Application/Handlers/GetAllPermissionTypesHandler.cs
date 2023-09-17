using MediatR;
using n5now_api.Application.DTOs;
using n5now_api.Infrastructure.Queries;
using n5now_api.Infrastructure.Repositories;

namespace n5now_api.Application.Handlers
{
    public class GetAllPermissionTypesHandler : IRequestHandler<GetAllPermissionTypesQuery, IEnumerable<PermissionTypeDto>>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public GetAllPermissionTypesHandler(IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionTypeRepository = permissionTypeRepository;
        }
        public async Task<IEnumerable<PermissionTypeDto>> Handle(GetAllPermissionTypesQuery request, CancellationToken cancellationToken)
        {
            return await _permissionTypeRepository.GetAllPermissionTypes();
        }
    }
}
