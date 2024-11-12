using api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace api.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly APIDbContext _context;
        public PersonRepository(APIDbContext context) => _context = context;

        public async Task<IEnumerable<Person>> GetAllAsync() => await _context.Persons.ToListAsync();
        public async Task<Person> GetByIdAsync(int id) => await _context.Persons.FindAsync(id);
        public async Task AddAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Person>> GetPersonsWithPetsAsync()
        {
            return await _context.Persons.Include(p => p.Pets).Where(p => p.Pets.Any()).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetAllAsync(string? searchTerm = null)
        {
            return await _context.Persons
                .Include(p => p.Pets)
                .Where(p => string.IsNullOrEmpty(searchTerm) || p.FirstName.StartsWith(searchTerm))
                .ToListAsync();
        }

    }

}
