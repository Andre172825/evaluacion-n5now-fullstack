using MediatR;
using n5now_api.Application.DTOs;
using n5now_api.Domain.Models;
using n5now_api.Infrastructure.Commands;
using n5now_api.Infrastructure.Data;
using n5now_api.Infrastructure.Repositories;

namespace n5now_api.Application.Handlers
{
    public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, PermissionDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PermissionDto> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = new Permission();
            permission.Id = request.Id;
            permission.EmployeeName = request.EmployeeName;
            permission.EmployeeLastName = request.EmployeeLastName;
            permission.PermissionTypeId = request.PermissionTypeId;
            var response = await _unitOfWork.PermissionRepository.UpdatePermission(permission);
            await _unitOfWork.Save();
            return response;

        }
    }
}
