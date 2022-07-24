using Demo.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "code is required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "code is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Name Length must be between 6 to 100")]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
