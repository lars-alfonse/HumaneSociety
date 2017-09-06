using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Customer : User
    {
        Client client;
        public override void LogIn()
        {
            if (CheckIfNewUser())
            {
                CustomerInterface.CreateClient();
                LogInPreExistingUser();
            }
            else
            {
                Console.Clear();
                LogInPreExistingUser();
            }
            RunUserMenus();
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
                client = Query.GetClient(userName, password);
                name = client.firstName;
            }
            catch
            {
                UserInterface.DisplayUserOptions("User not found. Please try another username, contact support or type 'reset' to restart");
                LogIn();
                return;
            }
        }
        protected override void RunUserMenus()
        {
            List<string> options = new List<string>() { "1. Search for animals", "2. Update info", "3. Apply for Adoption", "4. Check Adoption Status" };
            Console.Clear();
            CheckIfAccountComplete();
            UserInterface.DisplayUserOptions(options);
            int input = UserInterface.GetIntegerData();
            RunUserInput(input);
            
        }

        private void RunUserInput(int input)
        {
            switch (input)
            {
                case 1:
                    RunSearch();
                    RunUserMenus();
                    return;
                case 2:
                    CustomerInterface.UpdateClientInfo(client);
                    RunUserMenus();
                    return;
                case 3:
                    ApplyForAdoption();
                    RunUserMenus();
                    return;
                case 4:
                    CheckAdoptionStatus();
                    RunUserMenus();
                    return;
                default:
                    UserInterface.DisplayUserOptions("Input not accepted please try again");
                    return;
            }
        }

        private void CheckAdoptionStatus()
        {
            var pendingAdoptions = Query.GetUserAdoptionStatus(client).ToList();
            if (pendingAdoptions.Count == 0)
            {
                UserInterface.DisplayUserOptions("No adoptions currently pending");
            }
            else
            {
                List<string> Adoptions = new List<string>();
                foreach(ClientAnimalJunction junction in pendingAdoptions)
                {
                    Adoptions.Add(junction.Animal1.name + " " + junction.Animal1.Breed1.breed1 + " " + junction.approvalStatus);
                }
                UserInterface.DisplayUserOptions(Adoptions);
                UserInterface.DisplayUserOptions("press enter to continue");
                Console.ReadLine();
            }
        }

        private void ApplyForAdoption()
        {
            Console.Clear();
            UserInterface.DisplayUserOptions("Please enter the ID of the animal you wish to adopt or type reset or exit");
            int iD = UserInterface.GetIntegerData();
            var animal = Query.GetAnimalByID(iD);
            UserInterface.DisplayAnimalInfo(animal);
            UserInterface.DisplayUserOptions("Would you like to adopt?");
            if ((bool)UserInterface.GetBitData())
            {
                Query.Adopt(animal, client);
                UserInterface.DisplayUserOptions("Adoption request sent we will hold $75 adoption fee until processed");
            }
        }

        private void RunSearch()
        {
            Console.Clear();
            var animals = SearchForAnimal().ToList();
            if (animals.Count > 1)
            {
                UserInterface.DisplayUserOptions("Several animals found");
                UserInterface.DisplayAnimals(animals);
            }
            else if(animals.Count == 0)
            {
                UserInterface.DisplayUserOptions("No animals found please try another search");
            }
            else
            {
                UserInterface.DisplayAnimalInfo(animals[0]);
            }
            UserInterface.DisplayUserOptions("Press enter to continue");
            Console.ReadLine();
        }

        private void CheckIfAccountComplete()
        {
            if(client.homeSize == null || client.kids == null || client.income == null)
            {
                UserInterface.DisplayUserOptions("Account not up to date would you like to update your account?");
                string input = UserInterface.GetUserInput();
                if (input == "yes" || input == "y")
                {
                    Console.Clear();
                    CustomerInterface.UpdateClientInfo(client);
                }
                else
                {
                    Console.Clear();
                    return;
                }
            }
        }
    }
}
