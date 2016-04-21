using HumanResourcesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.ConsoleApp
{
    class ConsoleController
    {
        private IRepository repository;

        public ConsoleController(IRepository repository)
        {
            this.repository = repository;
        }

        public void ListEmployees()
        {
            Console.WriteLine();
            var employees = repository.Employees;
            foreach (var empl in employees)
            {
                Console.WriteLine("{0} {1}, {4} {2}, {3}, Chief: {5}"
                    , empl.FirstName
                    , empl.SecondName
                    , empl.Role
                    , empl.DepartmentName
                    , empl.Rank
                    , empl.ChiefName
                );
            }
        }

        public void ListDepartments()
        {
            Console.WriteLine();
            var departments = repository.Departments;
            foreach (var dept in departments)
            {
                Console.WriteLine("{0} {1}", dept.Id, dept.Name);
            }
        }

        public void PromoteEmployee(int employeeId)
        {
            var employee = repository.Employees.Single(e => e.Id == employeeId);
            employee.Promote();
            repository.Save();
        }

        public void DemoteEmployee(int employeeId)
        {
            var employee = repository.Employees.Single(e => e.Id == employeeId);
            employee.Demote();
            repository.Save();
        }

        public void ShowEmployee(int employeeId)
        {
            var employee = repository.Employees.Single(e => e.Id == employeeId);
            Console.WriteLine("{0} {1}, {4} {2}, {3}, Chief: {5}"
                    , employee.FirstName
                    , employee.SecondName
                    , employee.Role
                    , employee.DepartmentName
                    , employee.Rank
                    , employee.ChiefName
            );
            foreach (var contact in employee.Contacts)
            {
                Console.WriteLine("  {0}: {1}", contact.Kind, contact.Info);
            }
        }

        public void ShowDepartment(int departmentId)
        {
            var department = repository.Departments.Single(d => d.Id == departmentId);
            Console.WriteLine("{0} {1}", department.Id, department.Name);
        }

        internal void AddContactInfo(int employeeId, string contactKind, string contactInfo)
        {            
            var employee = repository.Employees.Single(e => e.Id == employeeId);
            employee.AddContact(new ContactInfo { Kind = contactKind, Info = contactInfo });
            repository.Save();
        }

        internal void ChangeDepartment(int employeeId, int departmentId)
        {
            var employee = repository.Employees.Single(e => e.Id == employeeId);
            var department = repository.Departments.Single(d => d.Id == departmentId);
            employee.ChangeDepartment(department);
            repository.Save();
        }
    }
}
