namespace client.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<PetDTO> Pets { get; set; } = new List<PetDTO>();
    }
}
