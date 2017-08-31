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
    }
}
