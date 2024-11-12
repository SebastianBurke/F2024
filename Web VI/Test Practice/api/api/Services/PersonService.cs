using api.DTOs;
using api.Models;
using api.Repositories;

namespace api.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository) => _repository = repository;

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var persons = await _repository.GetAllAsync();
            return persons.Select(p => new PersonDTO { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName });
        }

        public async Task<PersonDTO> GetByIdAsync(int id)
        {
            var person = await _repository.GetByIdAsync(id);
            return person == null ? null : new PersonDTO { Id = person.Id, FirstName = person.FirstName, LastName = person.LastName };
        }

        public async Task AddAsync(PersonDTO personDTO)
        {
            var person = new Person { FirstName = personDTO.FirstName, LastName = personDTO.LastName };
            await _repository.AddAsync(person);
        }

        public async Task UpdateAsync(int id, PersonDTO personDTO)
        {
            var person = new Person { Id = id, FirstName = personDTO.FirstName, LastName = personDTO.LastName };
            await _repository.UpdateAsync(person);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<IEnumerable<PersonDTO>> GetPersonsWithPetsAsync()
        {
            var persons = await _repository.GetPersonsWithPetsAsync();
            return persons.Select(p => new PersonDTO
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Pets = p.Pets.Select(pet => new PetDTO { Id = pet.Id, Name = pet.Name, Type = pet.Type }).ToList()
            });
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync(string? searchTerm = null)
        {
            var persons = await _repository.GetAllAsync(searchTerm);
            return persons.Select(p => new PersonDTO
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Pets = p.Pets.Select(pet => new PetDTO
                {
                    Id = pet.Id,
                    Name = pet.Name,
                    Type = pet.Type
                }).ToList()
            });
        }
    }

}
