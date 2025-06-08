using APIGallery.Context;
using APIGallery.Models;
using APIGallery.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIGallery.Repositorios
{
    public class WorkArtRepository : IWorkArtRepository
    {
        private readonly ContextProject _context;

        public WorkArtRepository(ContextProject context)
        {
            _context = context;
        }

        public async Task<WorkArt> Create(WorkArt model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }
        public async Task<WorkArt> GetById(int id)
        {
            return await _context.WorkArts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<WorkArt>> GetAll()
        {
            return await _context.WorkArts.ToListAsync();
        }
        public async Task<WorkArt> Update(WorkArt model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var model = await _context.WorkArts.FindAsync(id);

            if (model == null)
            {
                return false;
            }

            _context.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}
