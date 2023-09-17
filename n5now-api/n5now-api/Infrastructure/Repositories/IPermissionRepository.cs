using n5now_api.Application.DTOs;
using n5now_api.Domain.Models;

namespace n5now_api.Infrastructure.Repositories
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<PermissionDto>> GetAllPermissions();
        Task<PermissionDto> GetPermissionById(int id);
        Task<Permission> CreatePermission(Permission permission);
        Task<PermissionDto> UpdatePermission(Permission permission);
        Task<int> DeletePermission(int id);
    }
}
