using APIGallery.Context;
using APIGallery.Interfaces;
using APIGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGallery.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ContextProject _context;

        public UsuarioRepository(ContextProject context)
        {
            _context = context;
        }
   
        public async Task<Usuario> ObterPeloEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c => c.Email == email);
        }

    }
}
