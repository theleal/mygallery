using APIGallery.Context;
using APIGallery.Interfaces;
using APIGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGallery.Repositorios
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextProject _context;

        public UserRepository(ContextProject context)
        {
            _context = context;
        }
   
        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Email == email);
        }

    }
}
