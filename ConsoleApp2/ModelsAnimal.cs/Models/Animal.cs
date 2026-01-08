namespace ZooManagementApp.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double FoodKg { get; set; }

        public Animal(int id, string name, string species, double foodKg)
        {
            Id = id;
            Name = name;
            Species = species;
            FoodKg = foodKg;
        }
    }
}