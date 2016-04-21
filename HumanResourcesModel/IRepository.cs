using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesModel
{
    public interface IRepository
    {
        IEnumerable<Employee> Employees { get; }
        IEnumerable<Department> Departments { get; }

        void AddEmployee(Employee employee);
        void RemoveEmployee(Employee employee);

        void AddDepartment(Department department);
        void RemoveDepartment(Department department);

        void Save();
    }
}
