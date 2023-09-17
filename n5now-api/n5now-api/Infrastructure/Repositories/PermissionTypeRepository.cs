using Microsoft.EntityFrameworkCore;
using n5now_api.Application.DTOs;
using n5now_api.Infrastructure.Data;

namespace n5now_api.Infrastructure.Repositories
{
    public class PermissionTypeRepository : IPermissionTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PermissionTypeDto>> GetAllPermissionTypes()
        {
            return await _context.PermissionTypes.Select(x => new PermissionTypeDto
            {
                Id = x.Id,
                Description = x.Description,
            }).ToListAsync();
        }

        public async Task<string> GetPermissionTypeDescriptionById(int id)
        {
            return await _context.PermissionTypes.Where(x => x.Id == id).Select(x => x.Description).FirstOrDefaultAsync();
        }
    }
}
