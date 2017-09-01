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
            UserInterface.DisplayUserOptions("Please enter your email");
            string email = UserInterface.GetUserInput();
            if(email.Contains("@") && email.Contains("."))
            {
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
        public static bool CreateClient()
        {
            try
            {
                var clients = Query.RetrieveClients();
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
        public static void  UpdateClientInfo(Client client)
        {
            //add columns

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
    }
}
