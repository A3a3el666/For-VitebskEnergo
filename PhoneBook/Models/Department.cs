using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Models
{
    [Table("departments")]
    public class Department
    {
        [Key]
        [Column("departmentid")]
        public int DepartmentId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("parentdepartmentid")]
        public int? ParentDepartmentId { get; set; }
    }
}