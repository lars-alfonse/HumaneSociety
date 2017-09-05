using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class UserInterface
    {
        public static void DisplayUserOptions(List<string> options)
        {
            foreach(string option in options)
            {
                Console.WriteLine(option);
            }
        }
        public static void DisplayUserOptions(string options)
        {
            Console.WriteLine(options);
        }
        public static string GetUserInput()
        {
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "reset":
                    PointOfEntry.Run();
                    Environment.Exit(1);
                    break;
                case "exit":
                    Environment.Exit(1);
                    break;
                default:
                    break;
            }

            return input;
        }
        public static string GetStringData(string parameter, string target)
        {
            string data;
            DisplayUserOptions($"What is {target} {parameter}?");
            data = GetUserInput();
            return data;
        }

        internal static bool? GetBitData(List<string> options)
        {
            DisplayUserOptions(options);
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static bool? GetBitData(string target, string parameter)
        {
            DisplayUserOptions($"Is {target} {parameter}?");
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int GetIntegerData(string parameter, string target)
        {
            try
            {
                int data = int.Parse(GetStringData(parameter, target));
                return data;
            }
            catch
            {
                Console.Clear();
                DisplayUserOptions("Incorrect input please enter an integer number.");
                return GetIntegerData(parameter, target);
            }
        }
    }
}
