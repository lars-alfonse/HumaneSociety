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
            Console.Clear();
            CheckIfAccountComplete();
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
