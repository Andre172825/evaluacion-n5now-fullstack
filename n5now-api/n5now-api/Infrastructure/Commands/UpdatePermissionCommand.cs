﻿using MediatR;
using n5now_api.Application.DTOs;

namespace n5now_api.Infrastructure.Commands
{
    public record UpdatePermissionCommand(int Id, string EmployeeName, string EmployeeLastName, int PermissionTypeId) : IRequest<PermissionDto>;
}
