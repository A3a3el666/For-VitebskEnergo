using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Models
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        [Column("employeeid")]
        public int EmployeeId { get; set; }

        [Required]
        [Column("departmentid")]
        public int DepartmentId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("position")]
        public string Position { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("email")]
        public string Email { get; set; }
    }
}