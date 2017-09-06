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
                case "2":
                    RemoveAnimal();
                    RunUserMenus();
                    return;
                case "3":
                    CheckAnimalStatus();
                    RunUserMenus();
                    return;
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

        private void CheckAnimalStatus()
        {
            Console.Clear();
            var animals = SearchForAnimal().ToList();
            if(animals.Count > 1)
            {
                UserInterface.DisplayUserOptions("Several animals found");
                UserInterface.DisplayAnimals(animals);
                UserInterface.DisplayUserOptions("Enter the ID of the animal you would like to check");
                int ID = UserInterface.GetIntegerData();
                CheckAnimalStatus(ID);
                return;
            }
            if(animals.Count == 0)
            {
                UserInterface.DisplayUserOptions("Animal not found please use different search criteria");
                return;
            }
            RunCheckMenu(animals[0]);
        }

        private void RunCheckMenu(Animal animal)
        {
            bool isFinished = false;
            Console.Clear();
            while(!isFinished){
                List<string> options = new List<string>() { "Animal found:", animal.name, animal.Breed1.Species1.species, animal.Breed1.breed1, animal.Breed1.pattern, "Would you like to:", "1. Get Info", "2. Update Info", "3. Check shots", "4. Return" };
                UserInterface.DisplayUserOptions(options);
                int input = UserInterface.GetIntegerData();
                if (input == 4)
                {
                    isFinished = true;
                    continue;
                }
                RunCheckMenuInput(input, animal);
            }
        }

        private void RunCheckMenuInput(int input, Animal animal)
        {
            
            switch (input)
            {
                case 1:
                    UserInterface.DisplayAnimalInfo(animal);
                    Console.Clear();
                    return;
                case 2:
                    Console.Clear();
                    return;
                case 3:
                    Console.Clear();
                    return;
                default:
                    UserInterface.DisplayUserOptions("Input not accepted please select a menu choice");
                    return;
            }
        }

        private void CheckAnimalStatus(int iD)
        {
            Console.Clear();
            var animals = SearchForAnimal(iD).ToList();
            if (animals.Count == 0)
            {
                UserInterface.DisplayUserOptions("Animal not found please use different search criteria");
                return;
            }
            RunCheckMenu(animals[0]);
        }

        private IQueryable<Animal> SearchForAnimal(int iD)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
             var animals = (from data in context.Animals where data.ID == iD select data);
            return animals;
        }

        private void RemoveAnimal()
        {

            var animals = SearchForAnimal().ToList();
            if (animals.Count > 1)
            {
                UserInterface.DisplayUserOptions("Several animals found please refine your search.");
                UserInterface.DisplayAnimals(animals);
                UserInterface.DisplayUserOptions("Press enter to continue");
                Console.ReadLine();
                return;
            }
            else if (animals.Count < 1)
            {
                UserInterface.DisplayUserOptions("Animal not found please use different search criteria");
                return;
            }
            var animal = animals[0];
            List<string> options = new List<string>() { "Animal found:", animal.name, animal.Breed1.breed1, "would you like to delete?" };
            if ((bool)UserInterface.GetBitData(options))
            {
                Query.RemoveAnimal(animal);
            }
        }

        private IQueryable<Animal> SearchForAnimal()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var animals = from data in context.Animals select data;

            var searchParameters = GetAnimalCriteria();
            if (searchParameters.ContainsKey(1))
            {
                animals = (from data in animals where data.Breed1.Species1.species == searchParameters[1] select data);
            }
            if (searchParameters.ContainsKey(2))
            {
                animals = (from data in animals where data.Breed1.breed1 == searchParameters[2] select data);
            }
            if (searchParameters.ContainsKey(3))
            {
                animals = (from data in animals where data.name == searchParameters[3] select data);
            }
            if (searchParameters.ContainsKey(4))
            {
                animals = (from data in animals where data.age == int.Parse(searchParameters[4]) select data);
            }
            if (searchParameters.ContainsKey(5))
            {
                animals = (from data in animals where data.demeanor == searchParameters[5] select data);
            }
            if (searchParameters.ContainsKey(6))
            {
                bool parameter = GetBoolParamater(searchParameters[6]);
                animals = (from data in animals where data.kidFriendly == parameter select data);
            }
            if (searchParameters.ContainsKey(7))
            {
                bool parameter = GetBoolParamater(searchParameters[7]);
                animals = (from data in animals where data.petFriendly == parameter select data);
            }
            if (searchParameters.ContainsKey(8))
            {
                animals = (from data in animals where data.weight == int.Parse(searchParameters[8]) select data);
            }
            if (searchParameters.ContainsKey(8))
            {
                animals = (from data in animals where data.weight == int.Parse(searchParameters[8]) select data);
            }
            if (searchParameters.ContainsKey(9))
            {
                animals = (from data in animals where data.ID == int.Parse(searchParameters[9]) select data);
            }
            return animals;
        }

        private bool GetBoolParamater(string input)
        {
            if (input.ToLower() == "true")
            {
                return true;
            }
            else
            {
                return false;
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
            try
            {
                employee = Query.RetrieveEmployeeUser(email, employeeNumber);
            }
            catch
            {
                UserInterface.DisplayUserOptions("Employee not found please contact your administrator");
                PointOfEntry.Run();
            }
            if (employee.pass != null)
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
            if (Query.CheckEmployeeUserNameExist(username))
            {
                UserInterface.DisplayUserOptions("Username already in use please try another username.");
                GetUserName();
            }
            else
            {
                employee.userName = username;
                UserInterface.DisplayUserOptions("Username successful");
            }
        }
    }
}
