using n5now_api.Application.DTOs;

namespace n5now_api.Infrastructure.Repositories
{
    public interface IPermissionTypeRepository
    {
        Task<IEnumerable<PermissionTypeDto>> GetAllPermissionTypes();
        Task<string> GetPermissionTypeDescriptionById(int id);
    }
}
