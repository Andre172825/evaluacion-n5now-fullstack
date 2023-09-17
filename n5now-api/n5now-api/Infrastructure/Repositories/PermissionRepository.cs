using Microsoft.EntityFrameworkCore;
using n5now_api.Application.DTOs;
using n5now_api.Domain.Models;
using n5now_api.Infrastructure.Data;
using Nest;
using System.Security;

namespace n5now_api.Infrastructure.Repositories
{
    public class PermissionRepository: IPermissionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ElasticClient _client;

        public PermissionRepository(ApplicationDbContext context, IESClientProvider clientProvider)
        {
            _context = context;
            _client = clientProvider.GetClient();
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissions()
        {
            var searchResponse = await _client.SearchAsync<PermissionDto>(s => s
             .MatchAll()
             
            );

            if (searchResponse.IsValid)
            {
                var permissions = searchResponse.Documents.Select(x => new PermissionDto
                {
                    Id = x.Id,
                    EmployeeName = x.EmployeeName,
                    EmployeeLastName = x.EmployeeLastName,
                    PermissionTypeId = x.PermissionTypeId,
                    PermissionTypeDescription = x.PermissionTypeDescription,
                    PermissionDate = x.PermissionDate
                });

                return permissions;
            }
            else
            {
                throw new Exception($"Error al obtener permisos desde Elasticsearch: {searchResponse.ServerError}");
            }
        }

        public async Task<PermissionDto> GetPermissionById(int id)
        {
            var response = await _client.GetAsync<PermissionDto>(id, g => g.Index("demo"));

            if (response.IsValid)
            {
                return response.Source;
            }
            else
            {
                return null;
            }
        }

        public async Task<Permission> CreatePermission(Permission permission)
        {
            _context.Permissions.Add(permission);
            return permission;
        }

        public async Task<PermissionDto> UpdatePermission(Permission permission)
        {
            var _permission = await _context.Permissions.Include(x => x.PermissionType).FirstOrDefaultAsync(x => x.Id == permission.Id)!;
            
            if(permission == null)
            {
                return null;
            }
            else
            {
                _permission.EmployeeName = permission.EmployeeName;
                _permission.EmployeeLastName = permission.EmployeeLastName;
                _permission.PermissionTypeId = permission.PermissionTypeId;

                return new PermissionDto
                {
                    Id = permission.Id,
                    EmployeeName = permission.EmployeeName,
                    EmployeeLastName = permission.EmployeeLastName,
                    PermissionTypeId = permission.PermissionTypeId,
                    PermissionTypeDescription = _permission.PermissionType!.Description,
                    PermissionDate = permission.PermissionDate,
                };
            }            
        }

        public async Task<int> DeletePermission(int id)
        {
            var permission = await _context.Permissions.FirstOrDefaultAsync(x => x.Id == id);
            if (permission != null)
            {
                await _client.DeleteAsync<Permission>(permission.Id.ToString());
                _context.Permissions.Remove(permission);
                return id;
            }
            else
            {
                return 0;
            }
        }
    }
}
