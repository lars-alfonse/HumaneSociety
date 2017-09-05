using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class User
    {
        protected string name;
        protected int access;
        protected string userName;

        public virtual void LogIn()
        {

        }
        protected bool CheckIfNewUser()
        {
            List<string> options = new List<string>() { "Are you a new User?", "yes", "no" };
            UserInterface.DisplayUserOptions(options);
            string input = UserInterface.GetUserInput();
            if(input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else if(input.ToLower() == "no" || input.ToLower() == "n")
            {
                return false;
            }
            else
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Input not recognized please try again");
                return CheckIfNewUser();
            }
        }
        protected virtual void LogInPreExistingUser()
        {

        }
        protected virtual void RunUserMenus()
        {

        }
        protected Dictionary<int, string> GetAnimalCriteria()
        {
            Dictionary<int, string> searchParameters = new Dictionary<int, string>();
            bool isSearching = true;
            while (isSearching)
            {
                Console.Clear();
                List<string> options = new List<string>() { "Select Search Criteia: (Enter number and choose finished when finished)", "1. Species", "2. Breed", "3. Name", "4. Age", "5. Demeanor", "6. Kid friendly", "7. Pet friendly", "8. Weight","9. ID", "10. Finished" };
                UserInterface.DisplayUserOptions(options);
                string input = UserInterface.GetUserInput();
                if (input.ToLower() == "10" || input.ToLower() == "finished")
                {
                    isSearching = false;
                    continue;
                }
                else
                {
                    searchParameters = CustomerInterface.EnterSearchCriteria(searchParameters, input);
                }
            }
            return searchParameters;
        }
    }
}
