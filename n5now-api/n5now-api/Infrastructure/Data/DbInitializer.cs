using n5now_api.Domain.Models;

namespace n5now_api.Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.PermissionTypes.Any()) return;

            var permissionTypes = new PermissionType[]
            {
                new PermissionType{Description = "Admin Access"},
                new PermissionType{Description = "Modify Access"},
                new PermissionType{Description = "Read Access"},
                new PermissionType{Description = "Delete Access"}
            };
            foreach (PermissionType s in permissionTypes)
            {
                context.PermissionTypes.Add(s);
            }
            context.SaveChanges();
        }
    }
}
