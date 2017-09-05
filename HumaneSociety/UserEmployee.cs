using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class UserEmployee : User
    {
        Employee employee;
        
        public override void LogIn()
        {
            if (CheckIfNewUser())
            {
                CreateNewEmployee();
                LogInPreExistingUser();
            }
            else
            {
                Console.Clear();
                LogInPreExistingUser();
            }
            RunUserMenus();
        }
        protected override void RunUserMenus()
        {
            List<string> options = new List<string>() { "What would you like to do? (select number of choice)", "1. Add animal", "2. Remove Anmial", "3. Check Animal Status",  "4. Approve Adoption" };
            UserInterface.DisplayUserOptions(options);
            string input = UserInterface.GetUserInput();
            RunUserInput(input);
        }
        private void RunUserInput(string input)
        {
            switch (input)
            {
                case "1":
                    AddAnimal();
                    RunUserMenus();
                    return;
                //case "2":
                //    RemoveAnimal();
                //    RunUserMenus();
                //    return;
                //case "3":
                //    CheckAnimalStatus();
                //    RunUserMenus();
                //    return;
                //case "4":
                //    CheckAdoptions();
                //    RunUserMenus();
                //    return;
                default:
                    UserInterface.DisplayUserOptions("Input not accepted please try again");
                    RunUserMenus();
                    return;
            }
        }

        private void AddAnimal()
        {
            Console.Clear();
            Animal animal = new Animal();
            animal.breed = Query.GetBreed();
            animal.name = UserInterface.GetStringData("name", "the animal's");
            animal.age = UserInterface.GetIntegerData("age", "the animal's");
            animal.demeanor = UserInterface.GetStringData("demeanor", "the animal's");
            animal.kidFriendly = UserInterface.GetBitData("the animal", "child friendly");
            animal.petFriendly = UserInterface.GetBitData("the animal", "pet friendly");
            animal.weight = UserInterface.GetIntegerData("the animal's", "weight");
            animal.diet = Query.GetDiet();
            animal.location = Query.GetLocation();
            Query.AddAnimal(animal);
        }
        protected override void LogInPreExistingUser()
        {
            List<string> options = new List<string>() { "Please log in", "Enter your username (CaSe SeNsItIvE)" };
            UserInterface.DisplayUserOptions(options);
            userName = UserInterface.GetUserInput();
            UserInterface.DisplayUserOptions("Enter your password (CaSe SeNsItIvE)");
            string password = UserInterface.GetUserInput();
            try
            {
                Console.Clear();
                employee = Query.EmployeeLogin(userName, password);
                UserInterface.DisplayUserOptions("Login successfull. Welcome.");
            }
            catch
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Employee not found, please try again, create a new user or contact your administrator");
                LogIn();
            }
            
        }
        private void CreateNewEmployee()
        {
            Console.Clear();
            string email = UserInterface.GetStringData("email", "your");
            int employeeNumber = int.Parse(UserInterface.GetStringData("employee number", "your"));
            employee = Query.RetrieveEmployeeUser(email, employeeNumber);
            if (employee.pass == null)
            {
                UserInterface.DisplayUserOptions("User already in use please log in or contact your administrator");
                LogIn();
                return;
            }
            else
            {
                UpdateEmployeeInfo();
            }
        }

        private void UpdateEmployeeInfo()
        {
            GetUserName();
            GetPassword();
            Query.AddUsernameAndPassword(employee);
        }

        private void GetPassword()
        {
            UserInterface.DisplayUserOptions("Please enter your password: (CaSe SeNsItIvE)");
            employee.pass = UserInterface.GetUserInput();
        }

        private void GetUserName()
        {
            Console.Clear();
            string username = UserInterface.GetStringData("username", "your");
            if (Query.CheckEmployeeUserNameExist(userName))
            {
                UserInterface.DisplayUserOptions("Username already in use please try another username.");
                GetUserName();
            }
            else
            {
                employee.userName = userName;
                UserInterface.DisplayUserOptions("Username successful");
            }
        }
    }
}
