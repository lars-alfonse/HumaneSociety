using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class UserInterface
    {
        private string userInput;

        public string UserInput
        {
            get
            {
                return userInput;
            }
        }

        protected virtual void DisplayUserOptions()
        {

        }
        protected virtual void GetUserInput()
        {

        }
        public virtual void RunMenu()
        {

        }
    }
}
