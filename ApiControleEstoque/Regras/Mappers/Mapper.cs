using ApiControleEstoque.Models;
using ApiControleEstoque.Models.Request;
using ApiControleEstoque.Models.Response;

namespace ApiControleEstoque.Regras.Mappers
{
    public class Mapper
    {
        public Mapper() { }

        public Estoque MapperRequestEstoqueModelToEstoqueModel(ProdutoRequestModel model)
        {
            Estoque estoque = new Estoque();

            estoque.Id = model.Id;
            estoque.Name = model.Name;
            estoque.Quantidade = model.Quantidade;

            return estoque;
        }
    }
}
