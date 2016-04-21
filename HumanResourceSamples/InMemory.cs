using HumanResourcesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceSamples
{
    class InMemory
    {
        static void Main(string[] args)
        {
            IRepository repo = new MemoryRepository();

            var employees = repo.Employees;
            PrintEmployees(employees);

            var emplArtur = repo.Employees.First(
                e => e.FirstName.Equals("Artur", StringComparison.InvariantCultureIgnoreCase)
            );
            emplArtur.Promote();
            repo.Save();
            PrintEmployees(repo.Employees);

            var engeneeringDepartment = new Department { Id = 4, Name = "Engineering" };
            repo.AddDepartment(engeneeringDepartment);
            repo.Save();
            PrintDepartments(repo.Departments);

            emplArtur.ChangeDepartment(engeneeringDepartment);
            repo.Save();
            PrintEmployees(repo.Employees);

            emplArtur.Demote();
            repo.Save();
            PrintEmployees(repo.Employees);

            var marcoCantu = employees.Single(e => e.FirstName.Equals("Marco") && e.SecondName.Equals("Cantu"));
            emplArtur.ChangeChief(marcoCantu);
            repo.Save();
            PrintEmployees(repo.Employees);

            Console.Read();
            Console.WriteLine("now working with file");
        }

        private static void Populate(IRepository repo)
        {
            var RnDdepartment = new Department { Id = 1, Name = "Research and Development" };
            repo.AddDepartment(RnDdepartment);
            var QADepartment = new Department { Id = 2, Name = "Quality Assurance" };
            repo.AddDepartment(QADepartment);
            var executiveDepartment = new Department { Id = 3, Name = "Executive" };
            repo.AddDepartment(executiveDepartment);

            repo.AddEmployee(new Employee(1, "Artur", "Ilin", "Developer", RnDdepartment, EmployeeRank.Junior));
            repo.AddEmployee(new Employee(2, "Marco", "Cantu", "Developer", RnDdepartment, EmployeeRank.Senior));
            repo.AddEmployee(new Employee(3, "Nick", "Hodges", "Evanelist", RnDdepartment, EmployeeRank.Middle));
            repo.AddEmployee(new Employee(4, "Uncle", "Bob", "QA Engineer", QADepartment, EmployeeRank.Senior));
            repo.AddEmployee(new Employee(5, "Steve", "McConnell", "Executive", executiveDepartment, EmployeeRank.Senior));
            repo.Save();
        }

        private static void PrintDepartments(IEnumerable<Department> departments)
        {
            Console.WriteLine();
            foreach (var dept in departments)
            {
                Console.WriteLine("{0} {1}", dept.Id, dept.Name);
            }
        }

        private static void PrintEmployees(IEnumerable<Employee> employees)
        {
            Console.WriteLine();
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
    }
}
