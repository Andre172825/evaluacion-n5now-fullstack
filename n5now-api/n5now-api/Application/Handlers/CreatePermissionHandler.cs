using MediatR;
using n5now_api.Application.DTOs;
using n5now_api.Domain.Models;
using n5now_api.Infrastructure.Commands;
using n5now_api.Infrastructure.Data;

namespace n5now_api.Application.Handlers
{
    public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, PermissionDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = new Permission(request.EmployeeName, request.EmployeeLastName, request.PermissionTypeId);
            await _unitOfWork.PermissionRepository.CreatePermission(permission);
            await _unitOfWork.Save();
            return new PermissionDto
            {
                Id = permission.Id,
                EmployeeName = permission.EmployeeName,
                EmployeeLastName = permission.EmployeeLastName,
                PermissionTypeId = permission.PermissionTypeId,
                PermissionTypeDescription = await _unitOfWork.PermissionTypeRepository.GetPermissionTypeDescriptionById(permission.PermissionTypeId),
                PermissionDate = permission.PermissionDate,
            };
        }
    }
}
