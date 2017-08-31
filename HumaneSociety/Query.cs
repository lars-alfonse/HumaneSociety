using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        public static List<Animal> GetAnimals()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var result = from r in context.Animals select r;
            return result.ToList();
        }
        public static void AddAnimal()
        {
            Console.WriteLine("What is the animals name");
            string name = Console.ReadLine();
            Animal animal = new Animal();
            animal.name = name;
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Animals.InsertOnSubmit(animal);
            context.SubmitChanges();
        }
        public static bool CreateClient()
        {
            try
            {

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
