using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace n5now_api.Domain.Models
{
    [Table("PermissionTypes")]
    public class PermissionType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string? Description { get; set; }
        public ICollection<Permission>? Permissions { get; set; }
    }
}
