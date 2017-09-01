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
            //Query.AddAnimal();
            //List<Animal> animals =  Query.GetAnimals();
            //foreach(Animal animal in animals)
            //{
            //    Console.WriteLine(animal.name);
            //}
            //CustomerInterface.CreateClient();
            //var clients = Query.RetrieveClients();
            //string Username = Console.ReadLine();
            //var client = from user in clients where user.userName == Username select user.firstName;
            //Console.WriteLine(client.ToList()[0]);
            //Console.ReadLine();
            //HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            //var map = context.Mapping.GetTables().ToList();
            //var personalInfo = from x in map where x.TableName == "dbo.Clients" select x;
            //foreach (var data in personalInfo.ToList())
            //{
            //    foreach (var item in data.RowType.DataMembers.ToList())
            //    {
            //        Console.WriteLine(item.MappedName);
            //    }
            //}
            //Console.ReadLine();
            //foreach (var item in map)
            //{
            //   foreach(var data in item.RowType.DataMembers.ToList())
            //    {
            //        Console.WriteLine(data.MappedName);
            //    }
            //}
            //Console.ReadLine();
            PointOfEntry.Run();
        }
    }
}
