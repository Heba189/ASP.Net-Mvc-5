using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitofWork : IUnitofWork
    {
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get ; set; }
        public UnitofWork(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            DepartmentRepository = departmentRepository;
            EmployeeRepository = employeeRepository;
        }
    }
}
