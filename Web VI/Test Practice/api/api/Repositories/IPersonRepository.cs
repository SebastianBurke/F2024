using api.Models;

namespace api.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetByIdAsync(int id);
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(int id);
        Task<IEnumerable<Person>> GetPersonsWithPetsAsync();
        Task<IEnumerable<Person>> GetAllAsync(string? searchTerm = null);
    }

}
