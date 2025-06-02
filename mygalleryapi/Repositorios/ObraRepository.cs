using APIGallery.Context;
using APIGallery.Interfaces;
using APIGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGallery.Repositorios
{
    public class ObraRepository : IObraRepository
    {
        private readonly ContextProject _context;

        public ObraRepository(ContextProject context)
        {
            _context = context;
        }

        public async Task<Obra> Atualizar(Obra model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Obra> Criar(Obra model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var model = await _context.Obras.FindAsync(id);

            if (model == null)
            {
                return false;
            }

            _context.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Obra> ObterPeloId(int id)
        {
            return await _context.Obras.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Obra>> ObterTodos()
        {
            return await _context.Obras.ToListAsync();
        }
    }
}
