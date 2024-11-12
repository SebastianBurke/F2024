using api.DTOs;

namespace api.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTO>> GetAllAsync();
        Task<PersonDTO> GetByIdAsync(int id);
        Task AddAsync(PersonDTO personDTO);
        Task UpdateAsync(int id, PersonDTO personDTO);
        Task DeleteAsync(int id);
        Task<IEnumerable<PersonDTO>> GetPersonsWithPetsAsync();
        Task<IEnumerable<PersonDTO>> GetAllAsync(string? searchTerm = null);
    }

}
