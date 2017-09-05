using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
   public static class CustomerInterface
    {
        public static string GetUserName()
        {
            UserInterface.DisplayUserOptions("Please enter a User name");
            string username = UserInterface.GetUserInput();
            var clients = Query.RetrieveClients();
            var clientUsernames = from client in clients select client.userName;
            if (CheckForForValue(clientUsernames.ToList(), username))
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Username already in use please try another username");
                return GetUserName();
            }
            return username;
        }
        public static bool CheckForForValue<T>(List<T> items, T value)
        {
            if (items.Contains(value))
            {
                return true;
            }
            return false;
        }
        public static string GetEmail()
        {
            var clients = Query.RetrieveClients();
            var clientEmails = from client in clients select client.email;
            UserInterface.DisplayUserOptions("Please enter your email");
            string email = UserInterface.GetUserInput();
            if(email.Contains("@") && email.Contains("."))
            {
                    if (CheckForForValue(clientEmails.ToList(), email))
                {
                    Console.Clear();
                    UserInterface.DisplayUserOptions("Email already in use please try another email or contact support for forgotten account info");
                    return GetEmail();
                }
                return email;
            }
            else
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Email not valid please enter a valid email address");
                return GetEmail();
            }

        }
        private static int GetState()
        {
            UserInterface.DisplayUserOptions("Please enter your state (abbreviation or full state name");
            string state = UserInterface.GetUserInput();
            var states = Query.GetStates();
            var stateNames = from territory in states select territory.name.ToLower();
            var stateAbrreviations = from territory in states select territory.abbrev;
            if(stateNames.ToList().Contains(state.ToLower()) || stateAbrreviations.ToList().Contains(state.ToUpper()))
            {
                try
                {
                    var stateReturn = from territory in states where territory.name.ToLower() == state.ToLower() select territory.ID;
                    int stateNumber = stateReturn.ToList()[0];
                    return stateNumber;
                }
                catch
                {
                    var stateReturn = from territory in states where territory.abbrev == state.ToUpper() select territory.ID;
                    int stateNumber = stateReturn.ToList()[0];
                    return stateNumber;
                }
            }
            else
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("State not Found");
                return GetState();
            }
        }

        internal static Dictionary<int, string> EnterSearchCriteria(Dictionary<int, string> searchParameters, string input)
        {
            Console.Clear();
            switch (input)
            {
                case "1":
                    searchParameters.Add(1 , UserInterface.GetStringData("species", "the animal's"));
                    return searchParameters;
                case "2":
                    searchParameters.Add(2, UserInterface.GetStringData("breed", "the animal's"));
                    return searchParameters;
                case "3":
                    searchParameters.Add(3, UserInterface.GetStringData("name", "the animal's"));
                    return searchParameters;
                case "4":
                    searchParameters.Add(4, UserInterface.GetIntegerData("age", "the animal's").ToString());
                    return searchParameters;
                case "5":
                    searchParameters.Add(5, UserInterface.GetStringData("demeanor", "the animal's"));
                    return searchParameters;
                case "6":
                    searchParameters.Add(6, UserInterface.GetBitData("the animal", "kid friendly").ToString());
                    return searchParameters;
                case "7":
                    searchParameters.Add(7, UserInterface.GetBitData("the animal", "pet friendly").ToString());
                    return searchParameters;
                case "8":
                    searchParameters.Add(8, UserInterface.GetIntegerData("weight", "the animal's").ToString());
                    return searchParameters;
                case "9":
                    searchParameters.Add(8, UserInterface.GetIntegerData("ID", "the animal's").ToString());
                    return searchParameters;
                default:
                    UserInterface.DisplayUserOptions("Input not recognized please try agian");
                    return searchParameters;
            }
        }

        public static bool CreateClient()
        {
            try
            {
                var clients = Query.RetrieveClients();
                var clientUsernames = from client in clients select client.userName;
                string username = GetUserName();
                string email = GetEmail();
                Console.Clear();
                UserInterface.DisplayUserOptions("Please enter password (Warning password is CaSe SeNsItIvE)");
                string password = UserInterface.GetUserInput();
                UserInterface.DisplayUserOptions("Enter your first name.");
                string firstName = UserInterface.GetUserInput();
                UserInterface.DisplayUserOptions("Enter your last name.");
                string lastName = UserInterface.GetUserInput();
                int zipCode = GetZipCode();
                int state = GetState();
                UserInterface.DisplayUserOptions("Please enter your street address");
                string streetAddress = UserInterface.GetUserInput();
                Query.AddNewClient(firstName, lastName, username, password, email, streetAddress, zipCode, state);
                Console.Clear();
                UserInterface.DisplayUserOptions("Profile successfully added");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CreateClient(IQueryable<Client> clients)
        {
            try
            {
                var clientUsernames = from client in clients select client.userName;
                var clientEmails = from client in clients select client.email;
                string username = GetUserName();
                string email = GetEmail();
                if (CheckForForValue(clientUsernames.ToList(), username))
                {
                    Console.Clear();
                    UserInterface.DisplayUserOptions("Username already in use please try another username");
                    return CreateClient(clients);
                }
                else if(CheckForForValue(clientEmails.ToList(), email))
                {
                    Console.Clear();
                    UserInterface.DisplayUserOptions("Email already in use please try another email or contact support for forgotten account info");
                    return CreateClient(clients);
                }
                else
                {
                    Console.Clear();
                    UserInterface.DisplayUserOptions("Please enter password (Warning password is CaSe SeNsItIvE)");
                    string password = UserInterface.GetUserInput();
                    UserInterface.DisplayUserOptions("Enter your first name.");
                    string firstName = UserInterface.GetUserInput();
                    UserInterface.DisplayUserOptions("Enter your last name.");
                    string lastName = UserInterface.GetUserInput();
                    int zipCode = GetZipCode();
                    int state = GetState();
                    UserInterface.DisplayUserOptions("Please enter your street address");
                    string streetAddress = UserInterface.GetUserInput();
                    Query.AddNewClient(firstName, lastName, username, password, email, streetAddress, zipCode, state);
                    Console.Clear();
                    UserInterface.DisplayUserOptions("Profile successfully added");

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void UpdateClientInfo(Client client)
        {
            List<string> options = new List<string>() { "What would you like to update? (Please enter number of option)", "1: Name", "2: Address", "3: Email", "4: Username", "5: Password", "6: Income", "7: Kids", "8: Home Size", "9. back" };
            int input = default(int);
            while (input != 9)
            {
                try
                {
                    UserInterface.DisplayUserOptions(options);
                    input = int.Parse(UserInterface.GetUserInput());
                    RunUpdateInput(input, client);
                }
                catch
                {
                    UserInterface.DisplayUserOptions("Input not recognized please enter an integer number of the option you would like to update");
                }
            }

      }
        private static void RunUpdateInput(int input, Client client)
        {
            switch (input)
            {
                case 1:
                    UpdateName(client);
                    break;
                case 2:
                    UpdateAddress(client);
                    break;
                case 3:
                    UpdateEmail(client);
                    break;
                case 4:
                    UpdateUsername(client);
                    break;
                case 5:
                    UpdatePassword(client);
                    break;
                case 6:
                    UpdateIncome(client);
                    break;
                case 7:
                    UpdateKids(client);
                    break;
                case 8:
                    UpdateHomeSize(client);
                    break;
                default:
                    UserInterface.DisplayUserOptions("You have reached this message in error please contact support or administator and give them code 10928849");
                    break;
            }

        }

        private static void UpdateHomeSize(Client client)
        {
            Console.Clear();
            UserInterface.DisplayUserOptions("What is your home size? (small, medium, large)");
        }

        private static void UpdateKids(Client client)
        {
            Console.Clear();
        }

        private static void UpdateIncome(Client client)
        {
            Console.Clear();
        }

        private static void UpdatePassword(Client client)
        {
            Console.Clear();
        }

        private static void UpdateUsername(Client client)
        {
            Console.Clear();
            UserInterface.DisplayUserOptions("Current Username: " + client.userName);
            client.userName = GetUserName();
            Query.UpdateUsername(client);
        }

        private static void UpdateEmail(Client client)
        {
            Console.Clear();
            UserInterface.DisplayUserOptions("Current email: " + client.email);
            client.email = GetEmail();
            Query.UpdateEmail(client);
        }

        public static int GetZipCode()
        {
            UserInterface.DisplayUserOptions("Please enter 5 digit zip");
            try
            {
                int zipCode = int.Parse(UserInterface.GetUserInput());
                return zipCode;
            }
            catch
            {
                UserInterface.DisplayUserOptions("Invalid Zip code please enter a 5 digit zipcode");
                return GetZipCode();
            }
        }
        public static void DisplayCurrentAddress(Client client)
        {
            string address = client.UserAddress1.addessLine1;
            string zipCode = client.UserAddress1.zipcode.ToString();
            string state = client.UserAddress1.USState1.name;
            UserInterface.DisplayUserOptions("Current address:");
            UserInterface.DisplayUserOptions($"{address}, {zipCode}, {state}");
        }
        public static void UpdateAddress(Client client)
        {
            Console.Clear();
            DisplayCurrentAddress(client);
            client.UserAddress1.zipcode = GetZipCode();
            client.UserAddress1.usState = GetState();
            UserInterface.DisplayUserOptions("Please enter your street address");
            client.UserAddress1.addessLine1 = UserInterface.GetUserInput();
            Query.UpdateAddress(client);

        }
        public static void UpdateName(Client client)
        {
            Console.Clear();
            List<string> options = new List<string>() { "Current Name:", client.firstName, client.lastName, "Would you like to update?", "1. First", "2. Last", "3. Both" };
            UserInterface.DisplayUserOptions(options);
            string input = UserInterface.GetUserInput().ToLower();
            if (input == "first" || input == "1")
            {
                UserInterface.DisplayUserOptions("Please enter your new first name.");
                client.firstName = UserInterface.GetUserInput();
                Query.UpdateFirstName(client);

            }
            else if (input == "last" || input == "2")
            {
                UserInterface.DisplayUserOptions("Please enter your new last name.");
                client.lastName = UserInterface.GetUserInput();
                Query.UpdateLastName(client);
            }
            else
            {
                UserInterface.DisplayUserOptions("Please enter your new first name.");
                client.firstName = UserInterface.GetUserInput();
                Query.UpdateFirstName(client);
                UserInterface.DisplayUserOptions("Please enter your new last name.");
                client.lastName = UserInterface.GetUserInput();
                Query.UpdateLastName(client);
            }
        }
    }
}
