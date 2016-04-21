using HumanResourcesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HumanResources.ConsoleApp
{
    class ConsoleApp
    {
        private static string[] COMMANDS_LIST = new[]
        {
            "list {employee|department}",
            "promote|demote {<id>}",
            "show {department|employee} {<id>}",
            "add contact {<employeeId>} kind=<contactKind> info=<contactInfo>",
            "move employee {<employeeId>} department {<departmentId>}"
        };

        private const string LIST_EMPLOYEES_COMMAND = "list employee";
        private const string LIST_DEPARTMENTS_COMMAND = "list department";
        private static Regex PROMOTE_COMMAND = new Regex(@"promote (\d+){1,9}", RegexOptions.IgnoreCase);
        private static Regex DEMOTE_COMMAND = new Regex(@"demote (\d+){1,9}", RegexOptions.IgnoreCase);
        private static Regex SHOW_EMPLOYEES_COMMAND = new Regex(@"show employee (\d+){1,9}", RegexOptions.IgnoreCase);
        private static Regex SHOW_DEPARTMENTS_COMMAND = new Regex(@"show department (\d+){1,9}", RegexOptions.IgnoreCase);
        private static Regex ADD_CONTACT_COMMAND= new Regex(@"add contact (\d+){1,9} kind=(\w+) info=(.+)", RegexOptions.IgnoreCase);
        private static Regex CHANGE_DEPARTMENT_COMMAND= new Regex(@"move employee (\d+){1,9} department (\d+){1,9}",RegexOptions.IgnoreCase);

        static void Main(string[] args)
        {
            //IRepository repo = new MemoryRepository();
            IRepository repo = new XmlRepository();
            ConsoleController controller = new ConsoleController(repo);

            var userInput = Console.ReadLine();
            while (userInput != "")
            {
                if (LIST_EMPLOYEES_COMMAND.Equals(userInput))
                {
                    controller.ListEmployees();
                }
                else if (LIST_DEPARTMENTS_COMMAND.Equals(userInput))
                {
                    controller.ListDepartments();
                }
                else if (PROMOTE_COMMAND.IsMatch(userInput))
                {
                    var match = PROMOTE_COMMAND.Match(userInput);
                    var employeeId = int.Parse(match.Groups[1].Value);
                    controller.PromoteEmployee(employeeId);
                }
                else if (DEMOTE_COMMAND.IsMatch(userInput))
                {
                    var match = DEMOTE_COMMAND.Match(userInput);
                    var employeeId = int.Parse(match.Groups[1].Value);
                    controller.DemoteEmployee(employeeId);
                }
                else if (SHOW_DEPARTMENTS_COMMAND.IsMatch(userInput))
                {
                    var match = SHOW_DEPARTMENTS_COMMAND.Match(userInput);
                    var departmentId = int.Parse(match.Groups[1].Value);
                    controller.ShowDepartment(departmentId);
                }
                else if (SHOW_EMPLOYEES_COMMAND.IsMatch(userInput))
                {
                    var match = SHOW_EMPLOYEES_COMMAND.Match(userInput);
                    var employeeId = int.Parse(match.Groups[1].Value);
                    controller.ShowEmployee(employeeId);
                }
                else if (ADD_CONTACT_COMMAND.IsMatch(userInput))
                {
                    var match = ADD_CONTACT_COMMAND.Match(userInput);
                    var employeeId = int.Parse(match.Groups[1].Value);
                    var contactKind = match.Groups[2].Value;
                    var contactInfo = match.Groups[3].Value;
                    controller.AddContactInfo(employeeId, contactKind, contactInfo);
                }
                else if (CHANGE_DEPARTMENT_COMMAND.IsMatch(userInput))
                {
                    var match = CHANGE_DEPARTMENT_COMMAND.Match(userInput);
                    var employeeId = int.Parse(match.Groups[1].Value);
                    var departmentId = int.Parse(match.Groups[2].Value);
                    controller.ChangeDepartment(employeeId, departmentId);
                }
                else
                {
                    ShowCommandsList();
                }
                Console.WriteLine();

                userInput = Console.ReadLine();
            }
        }

        private static void ShowCommandsList()
        {
            Console.WriteLine("Unknown Command, List of known Commands");
            foreach (var command in COMMANDS_LIST)
            {
                Console.WriteLine(command);
            }
        }
    }
}
