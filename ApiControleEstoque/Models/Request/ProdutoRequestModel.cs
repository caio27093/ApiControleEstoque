using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiControleEstoque.Models.Request
{
    public class ProdutoRequestModel
    {
        public ProdutoRequestModel()
        {

        }

        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int Quantidade { get; set; }
    }
}
