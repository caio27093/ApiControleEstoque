using ApiControleEstoque.Models;

namespace ApiControleEstoque.Interfaces
{
    public interface IEstoqueRepository
    {
        void Incluir(Estoque estoque);
        void Alterar(Estoque estoque);
        void Excluir(Estoque estoque);
        Task<Estoque> GetById(int id);
        Task<IEnumerable<Estoque>> SelecionarTodos();

        Task<bool> SaveAllAsync();
    }
}
