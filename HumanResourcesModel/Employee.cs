using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesModel
{
    [DataContract(IsReference = true)]
    public class Employee
    {
        [DataMember]
        private IList<ContactInfo> contacts = new List<ContactInfo>(5);
        [DataMember]
        private EmployeeRank rank = EmployeeRank.None;
        [DataMember]
        private Department department;
        private Employee _chief;
        [DataMember]
        private Employee chief
        {
            get
            {
                return _chief;
            }
            set
            {
                var oldChief = this._chief;
                oldChief?.RemoveSubordinate(this);
                _chief = value;
            }
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string SecondName { get; set; }
        [DataMember]
        public string Role { get; set; }
        public EmployeeRank Rank => rank;
        public IEnumerable<ContactInfo> Contacts => contacts;
        public string DepartmentName => department?.Name;
        public string ChiefName => chief?.FirstName;

        internal void AddSubordinate(Employee employee)
        {
            //TODO store subordinate Employees in list
        }

        internal void RemoveSubordinate(Employee employee)
        {
            //TODO remove subordinate Employees from list
        }

        public Employee(int id, string firstName, string secondName, string role,
            Department department, EmployeeRank rank)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.Role = role;
            this.department = department;
            this.rank = rank;
        }

        public void AddContact(ContactInfo contact)
        {
            Contract.Requires(contact != null);
            contacts.Add(contact);
        }

        public void RemoveContact(ContactInfo contact)
        {
            Contract.Requires(contact != null);
            contacts.Remove(contact);
        }

        public void Promote()
        {
            if (rank != EmployeeRank.Senior)
              rank++;
        }

        public void Demote()
        {
            if (rank != EmployeeRank.Junior)
              rank--;
        }

        public void ChangeDepartment(Department newDepartment)
        {
            this.department = newDepartment;
        }

        public void ChangeChief(Employee newChief)
        {
            this.chief = newChief;
        }
    }

    public enum EmployeeRank
    {
        None,
        Junior,
        Middle,
        Senior,
        RankError
    }
}
