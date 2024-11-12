namespace api.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int OwnerId { get; set; }

        public Person Owner { get; set; }
    }

}
