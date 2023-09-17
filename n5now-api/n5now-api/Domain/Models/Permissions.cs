using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace n5now_api.Domain.Models
{
    [Table("Permissions")]
    public class Permission
    {
        public Permission() { }
        public Permission(string? employeeName, string? employeeLastName, int permissionTypeId)
        {
            EmployeeName = employeeName;
            EmployeeLastName = employeeLastName;
            PermissionTypeId = permissionTypeId;
            PermissionDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(125)]
        public string? EmployeeName { get; set; }
        [MaxLength(125)]
        public string? EmployeeLastName { get; set; }
        public DateTime PermissionDate { get; set; }

        [ForeignKey("PermissionTypeId")]
        public int PermissionTypeId { get; set; }
        public PermissionType? PermissionType { get; set; }
    }
}
