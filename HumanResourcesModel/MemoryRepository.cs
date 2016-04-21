using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesModel
{
    public class MemoryRepository : IRepository
    {
        private IList<Department> departments = new List<Department>(6);
        public IEnumerable<Department> Departments => departments;

        private IList<Employee> employees = new List<Employee>(5);
        public IEnumerable<Employee> Employees => employees;

        public MemoryRepository()
        {
            PopoulateDepartments();
            PopulateEmployees();
        }

        private void PopulateEmployees()
        {
            employees.Add(new Employee(1, "Artur", "Ilin", "Developer", departments.First(), EmployeeRank.Junior));
            employees.Add(new Employee(2, "Marco", "Cantu", "Developer", departments.First(), EmployeeRank.Senior));
            employees.Add(new Employee(3, "Nick", "Hodges", "Evanelist", departments.First(), EmployeeRank.Middle));
            employees.Add(new Employee(4, "Uncle", "Bob", "QA Engineer", departments.First(d => d.Id == 2), EmployeeRank.Senior));
            employees.Add(new Employee(5, "Steve", "McConnell", "Executive", departments.First(d => d.Id == 3), EmployeeRank.Senior));
        }

        private void PopoulateDepartments()
        {
            departments.Add(new Department { Id = 1, Name = "Research and Development" });
            departments.Add(new Department { Id = 2, Name = "Quality Assurance" });
            departments.Add(new Department { Id = 3, Name = "Executive" });
        }

        public void AddDepartment(Department department)
        {
            Contract.Requires(department != null);
            departments.Add(department);
        }

        public void RemoveDepartment(Department department)
        {
            Contract.Requires(department != null);
            departments.Remove(department);
        }

        public void AddEmployee(Employee employee)
        {
            Contract.Requires(employee != null);
            employees.Add(employee);
        }


        public void RemoveEmployee(Employee employee)
        {
            Contract.Requires(employee != null);
            employees.Remove(employee);
        }

        public void Save()
        {
        }
    }
}
