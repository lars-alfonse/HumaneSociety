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
        public static void AddAnimal(Animal animal)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Animals.InsertOnSubmit(animal);
            context.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {

            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            Employee employee = (from data in context.Employees where data.email == email && data.employeeNumber == employeeNumber select data).First();
            return employee;
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var employee = (from data in context.Employees where data.userName == userName && data.pass == password select data).First();
            return employee;
        }

        public static IQueryable<USState> GetStates()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var states = from r in context.USStates select r;
            return states;
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var updateEmployee = (from data in context.Employees where data.email == employee.email select data).First();
            updateEmployee.pass = employee.pass;
            updateEmployee.userName = employee.userName;
            context.SubmitChanges();
        }

        internal static IQueryable<ClientAnimalJunction> GetPendingAdoptions()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var animals = from data in context.ClientAnimalJunctions where data.approvalStatus == "pending" select data;
            return animals;
        }

        internal static int? GetBreed()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            Breed breed = new Breed();
            Species species = new Species();
            species.species = UserInterface.GetStringData("species", "the animal's");
            context.Species.InsertOnSubmit(species);
            context.SubmitChanges();
            var currentSpecies = (from data in context.Species where data.species == species.species select species).First();
            breed.Species1 = currentSpecies;
            breed.breed1 = UserInterface.GetStringData("breed", "the animal's");
            breed.pattern = UserInterface.GetStringData("pattern", "the animial's");
            context.Breeds.InsertOnSubmit(breed);
            context.SubmitChanges();
            var currentBreed = (from data in context.Breeds where data.breed1 == breed.breed1 && data.Species1.species == breed.Species1.species && data.pattern == breed.pattern select data).First();
            return currentBreed.ID;
        }

        internal static IQueryable<ClientAnimalJunction> GetUserAdoptionStatus(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var adoptions = from data in context.ClientAnimalJunctions where data.client == client.ID select data;
            return adoptions;
        }

        public static void Adopt(Animal animal, Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            ClientAnimalJunction junction = new ClientAnimalJunction();
            junction.animal = animal.ID;
            junction.client = client.ID;
            junction.approvalStatus = "pending";
            context.ClientAnimalJunctions.InsertOnSubmit(junction);
            context.SubmitChanges();
        }

        public static Animal GetAnimalByID(int iD)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var animal = (from data in context.Animals where data.ID == iD select data).First();
            return animal;
        }

        internal static void UpdateAdoption(bool isApproved, ClientAnimalJunction junction)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var currentJunction = (from data in context.ClientAnimalJunctions where data.animal == junction.animal && data.client == junction.client select data).First();
            var currentAnimal = (from data in context.Animals where data.ID == junction.animal select data).First();
            if (isApproved)
            {
                currentJunction.approvalStatus = "approved";
                currentAnimal.adoptionStatus = "adopted";
                UserInterface.DisplayUserOptions("transferring adoption fee from adopter");
            }
            else
            {
                currentJunction.approvalStatus = "denied";
            }
            context.SubmitChanges();
        }

        public static void RemoveAnimal(Animal animal)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var removedAnimal = (from data in context.Animals where data.ID == animal.ID select data).First();
            context.Animals.DeleteOnSubmit(removedAnimal);
            context.SubmitChanges();
            
        }

        public static int GetDiet()
        {
            DietPlan plan = new DietPlan();
            plan.amount = UserInterface.GetIntegerData("amount", "the daily food");
            plan.food = UserInterface.GetStringData("the type of", "food");
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.DietPlans.InsertOnSubmit(plan);
            context.SubmitChanges();
            var currentPlan = (from data in context.DietPlans where data.amount == plan.amount && data.food == plan.food select data.ID).First();
            return currentPlan;
        }
        public static int GetLocation()
        {
            Room room = new Room();
            room.name = UserInterface.GetStringData("name", "the room");
            room.building = UserInterface.GetStringData("name", "the building");
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Rooms.InsertOnSubmit(room);
            context.SubmitChanges();
            var currentRoom = (from data in context.Rooms where data.name == room.name && data.building == room.building select data.ID).First();
            return currentRoom;

        }

        internal static void UpdateShot(string v, Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateShot(string v)
        {
            throw new NotImplementedException();
        }

        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var currentAnimal = (from data in context.Animals where data.ID == animal.ID select data).First();
            if (updates.ContainsKey(1))
            {
                currentAnimal.Breed1.Species1.species = updates[1];
            }
            if (updates.ContainsKey(2))
            {
                currentAnimal.Breed1.breed1 = updates[2];
            }
            if (updates.ContainsKey(3))
            {
                currentAnimal.name = updates[3];
            }
            if (updates.ContainsKey(4))
            {
                currentAnimal.age = int.Parse(updates[4]);
            }
            if (updates.ContainsKey(5))
            {
                currentAnimal.demeanor = updates[5];
            }
            if (updates.ContainsKey(6))
            {
                bool parameter = GetBoolParamater(updates[6]);
                currentAnimal.kidFriendly = parameter;
            }
            if (updates.ContainsKey(7))
            {
                bool parameter = GetBoolParamater(updates[7]);
                currentAnimal.petFriendly = parameter;
            }
            if (updates.ContainsKey(8))
            {
                currentAnimal.weight =int.Parse(updates[8]);
            }
            context.SubmitChanges();
        }
        private static bool GetBoolParamater(string input)
        {
            if (input.ToLower() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static IQueryable<AnimalShotJunction> GetShots(Animal animal)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var shots = (from data in context.AnimalShotJunctions where data.Animal_ID == animal.ID select data);
            return shots;
        }
        public static IQueryable<Client> RetrieveClients()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clients = from r in context.Clients select r;
            return clients;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var employee = (from data in context.Employees where data.userName == userName select data).SingleOrDefault();
            if (employee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void AddNewClient(string firstName, string lastName, string userName, string password, string email, string streetAddress, int zipCode, int stateNumber)
        {
            Client client = new Client();
            int address = GetClientAddressKey(streetAddress, zipCode, stateNumber);
            client.email = email;
            client.firstName = firstName;
            client.lastName = lastName;
            client.userName = userName;
            client.pass = password;
            client.userAddress = address;
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Clients.InsertOnSubmit(client);
            context.SubmitChanges();
        }

        internal static void RemoveEmployee(string lastName, string employeeNumber)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            Employee employee = (from data in context.Employees where data.lastName == lastName && data.employeeNumber == int.Parse(employeeNumber) select data).First();
            if (employee != null)
            {
                context.Employees.DeleteOnSubmit(employee);
                context.SubmitChanges();
            }
        }

        internal static void AddNewEmployee(string firstName, string lastName, string employeeNumber, string email)
        {
            Employee employee = new Employee();
            employee.employeeNumber = int.Parse(employeeNumber);
            employee.firsttName = firstName;
            employee.lastName = lastName;
            employee.email = email;
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Employees.InsertOnSubmit(employee);
            context.SubmitChanges();
        }

        public static Client GetClient(string username, string password)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var client = (from user in context.Clients where user.userName == username && user.pass == password select user).ToList();
            return (Client)client[0];
            
        }
        public static int GetClientAddressKey(string streetAddress, int zipCode, int stateNumber)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            int addressNumber;
            var addressObject = from address in context.UserAddresses where address.addessLine1 == streetAddress && address.zipcode == zipCode && address.usState == stateNumber select address.ID;
            if(addressObject.ToList().Count > 0)
            {
                addressNumber = addressObject.ToList()[0];
            }
            else
            {
                UserAddress address = new UserAddress();
                address.zipcode = zipCode;
                address.addessLine1 = streetAddress;
                address.usState = stateNumber;
                context.UserAddresses.InsertOnSubmit(address);
                context.SubmitChanges();
                var addressKey = from location in context.UserAddresses where location.addessLine1 == streetAddress && location.zipcode == zipCode && location.usState == stateNumber select address.ID;
                addressNumber = addressKey.ToList()[0];
            }
            return addressNumber;
        }

        public static void UpdateFirstName(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().firstName = client.firstName;
            context.SubmitChanges();
        }
        public static void UpdateLastName(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().lastName = client.lastName;
            context.SubmitChanges();
        }
        public static void UpdateAddress(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().UserAddress1.zipcode= client.UserAddress1.zipcode;
            clientData.First().UserAddress1.addessLine1 = client.UserAddress1.addessLine1;
            clientData.First().UserAddress1.usState = client.UserAddress1.usState;
            context.SubmitChanges();
        }
        public static void UpdateEmail(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().email = client.email;
            context.SubmitChanges();
        }
        public static void UpdateUsername(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().userName = client.userName;
            context.SubmitChanges();
        }
    }
}
