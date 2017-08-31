using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            Query.AddAnimal();
            List<Animal> animals =  Query.GetAnimals();
            foreach(Animal animal in animals)
            {
                Console.WriteLine(animal.name);
            }
            Console.ReadLine();
        }
    }
}
