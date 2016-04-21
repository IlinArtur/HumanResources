using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesModel
{
    public class XmlRepository : IRepository
    {
        private const string DEPARTMENTS_FILE = "Department.xml";
        private const string EMPLOYEES_FILE = "Employee.xml";
        private Lazy<IList<Department>> departments = 
            new Lazy<IList<Department>>(() => LoadData<Department>(DEPARTMENTS_FILE));

        private static IList<T> LoadData<T>(string fileName)
        {
            if (File.Exists(fileName))
            {
                IList<T> data;
                DataContractSerializer ser = new DataContractSerializer(typeof(IList<T>));
                using (var reader = File.OpenRead(fileName))
                {
                    data = ser.ReadObject(reader) as IList<T>;
                }
                //deserialization creates fixed size array, need to wrap it
                return new List<T>(data);
            }
            else
            {
                return new List<T>();
            }
        }

        private Lazy<IList<Employee>> employees = 
            new Lazy<IList<Employee>>(() => LoadData<Employee>(EMPLOYEES_FILE));

        public IEnumerable<Department> Departments => departments.Value;

        public IEnumerable<Employee> Employees => employees.Value;

        public void AddDepartment(Department department)
        {
            if (!departments.Value.Contains(department))
            {
                departments.Value.Add(department);
            }
        }

        public void RemoveDepartment(Department department)
        {
            departments.Value.Remove(department);
        }

        public void AddEmployee(Employee employee)
        {
            if (!employees.Value.Contains(employee))
            {
                employees.Value.Add(employee);
            }
        }

        public void RemoveEmployee(Employee employee)
        {
            employees.Value.Remove(employee);
        }

        public void Save()
        {
            SaveData(departments.Value, DEPARTMENTS_FILE);
            SaveData(employees.Value, EMPLOYEES_FILE);
        }

        private void SaveData<T>(IList<T> data, string fileName)
        {
            File.Delete(fileName);
            DataContractSerializer serializer = new DataContractSerializer(typeof(IList<T>));
            using (var writer = File.OpenWrite(fileName))
            {
                serializer.WriteObject(writer, data);
            }
        }
    }
}
