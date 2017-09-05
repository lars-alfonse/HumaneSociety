using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Admin : User
    {




        public override void LogIn()
        {
            UserInterface.DisplayUserOptions("What is your password?");
            string password = UserInterface.GetUserInput();
            if(password.ToLower() != "poiuyt")
            {
                UserInterface.DisplayUserOptions("Incorrect password please try again or type exit");
            }
            else
            {
                RunUserMenus();
            }
        }
        protected override void RunUserMenus()
        {
            Console.Clear();
            List<string> options = new List<string>() { "Admin log in successful.", "What would you like to do?", "1. Add new employee", "2. Remove employee", "(type 1, 2, add or remove)" };
            UserInterface.DisplayUserOptions(options);
            string input = UserInterface.GetUserInput();
            RunInput(input);
        }
        protected void RunInput(string input)
        {
            if(input == "1" || input.ToLower() == "add")
            {
                AddEmployee();
                RunUserMenus();
            }
            else if(input == "2" || input.ToLower() == "remove")
            {
                RemoveEmployee();
                RunUserMenus();
            }
            else
            {
                UserInterface.DisplayUserOptions("Input not recognized please try again or type exit");
                RunUserMenus();
            }
        }

        private void RemoveEmployee()
        {
            string lastName = GetEmployeeData("last name");
            string employeeNumber = GetEmployeeData("number");
            try
            {
                Console.Clear();
                Query.RemoveEmployee(lastName, employeeNumber);
                UserInterface.DisplayUserOptions("Employee successfully removed");
            }
            catch
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Employee removal unsuccessful please try again or type exit");
                RemoveEmployee();
            }
        }

        private void AddEmployee()
        {
            string firstName = GetEmployeeData("first name");
            string lastName = GetEmployeeData("last name");
            string employeeNumber = GetEmployeeData("number");
            string email = GetEmployeeData("email");
            try
            {
                Query.AddNewEmployee(firstName, lastName, employeeNumber, email);
                UserInterface.DisplayUserOptions("Employee addition successful.");
            }
            catch
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Employee addition unsuccessful please try again or type exit;");
                AddEmployee();
                return;
            }
        }

        private string GetEmployeeData(string parameter)
        {
            string data;
            UserInterface.DisplayUserOptions($"What is the employee's {parameter}?");
            data = UserInterface.GetUserInput();
            return data;
        }
    }
}
