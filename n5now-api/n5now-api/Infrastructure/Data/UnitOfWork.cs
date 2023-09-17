using n5now_api.Infrastructure.Repositories;

namespace n5now_api.Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        public IPermissionRepository PermissionRepository { get; }
        public IPermissionTypeRepository PermissionTypeRepository { get; }
        Task<int> Save();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IPermissionRepository PermissionRepository { get; }
        public IPermissionTypeRepository PermissionTypeRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IPermissionRepository permissionRepository, IPermissionTypeRepository permissionTypeRepository) 
        {
            _context = context;
            PermissionRepository = permissionRepository;
            PermissionTypeRepository = permissionTypeRepository;
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
