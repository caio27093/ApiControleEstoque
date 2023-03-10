using ApiControleEstoque.Interfaces;
using ApiControleEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ApiControleEstoque.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly ControleEstoqueContext _context;
        private readonly IMemoryCache _cache;

        public EstoqueRepository(IMemoryCache memoryCache, ControleEstoqueContext context)
        {
            _context = context;
            _cache = memoryCache;
        }

        public void Alterar(Estoque estoque)
        {
            _context.Entry(estoque).State = EntityState.Modified;
        }

        public void Excluir(Estoque estoque)
        {
            _context.Estoque.Remove(estoque);
        }

        public async Task<Estoque> GetById(int id)
        {
            return await _context.Estoque.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public void Incluir(Estoque estoque)
        {
            _context.Estoque.Add(estoque);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Estoque>> SelecionarTodos()
        {
            return await _context.Estoque.ToListAsync();
        }
    }
}
