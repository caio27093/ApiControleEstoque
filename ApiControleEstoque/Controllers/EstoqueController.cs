using ApiControleEstoque.Interfaces;
using ApiControleEstoque.Models;
using ApiControleEstoque.Models.Request;
using ApiControleEstoque.Models.Response;
using ApiControleEstoque.Regras.Mappers;
using ApiControleEstoque.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ApiControleEstoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoqueController : Controller
    {
        private readonly Mapper _mapper = new Mapper();
        private readonly IEstoqueRepository _estoqueRepository;

        public EstoqueController(IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        [HttpGet("ListEstoque")]
        public async Task<ActionResult<IEnumerable<Estoque>>> GetEstoques()
        {
            IEnumerable<Estoque> estoque = await _estoqueRepository.SelecionarTodos();
            return Ok(new ResponseModelPadrao<Estoque>() { IsSucces = true, Status = "Consulta de estoque realizada com sucesso.", Data = estoque.ToList() });
        }

        [HttpPost("GetEstoqueById")]
        public async Task<ActionResult> GetById(ByIdRequestModel model)
        {
            List<Estoque> estoques = new List<Estoque>();
            Estoque estoque = await _estoqueRepository.GetById(model.Id);
            if (estoque == null)
                return BadRequest(new ResponseModelPadrao<Estoque>() { IsSucces = false, Status = "Não foi possível consultar o estoque.", Data = null });

            estoques.Add(estoque);                

            return Ok(new ResponseModelPadrao<Estoque>() { IsSucces = true, Status = "Consulta de estoque realizada com sucesso.", Data = estoques });
        }

        [HttpPost("CadEstoque")]
        public async Task<ActionResult> CadastrarEstoque(ProdutoRequestModel model)
        {
            Estoque estoque = _mapper.MapperRequestEstoqueModelToEstoqueModel(model);

            _estoqueRepository.Incluir(estoque);

            if (await _estoqueRepository.SaveAllAsync())
                return Ok(new ResponseModelPadrao<Estoque>() { IsSucces = true, Status = "Estoque incluido com sucesso.", Data = null });

            return BadRequest(new ResponseModelPadrao<Estoque>() { IsSucces = false, Status = "Não foi possível incluir o estoque.", Data = null });
        }

        [HttpPut("AltEstoque")]
        public async Task<ActionResult> AlterarEstoque(ProdutoRequestModel model)
        {
            model = await ValidaCampos(model);

            Estoque estoque = _mapper.MapperRequestEstoqueModelToEstoqueModel(model);

            _estoqueRepository.Alterar(estoque);
            if (await _estoqueRepository.SaveAllAsync())
                return Ok(new ResponseModelPadrao<Estoque>() { IsSucces = true, Status = "Estoque alterado com sucesso.", Data = null });

            return BadRequest(new ResponseModelPadrao<Estoque>() { IsSucces = false, Status = "Não foi possível alterar o estoque.", Data = null });
        }

        [HttpDelete("DelEstoque")]
        public async Task<ActionResult> ExcluirEstoque(ByIdRequestModel model)
        {
            try
            {
                List<Estoque> estoques = new List<Estoque> ( );
                estoques.Add ( await _estoqueRepository.GetById ( model.Id ) );

                if (estoques[0].Id <= 0)
                    return NotFound ( "Estoque não encontrado" );

                _estoqueRepository.Excluir ( estoques[0] );
                if (await _estoqueRepository.SaveAllAsync ( ))
                    return Ok ( new ResponseModelPadrao<Estoque> ( ) { IsSucces = true, Status = "Estoque excluído com sucesso.", Data = null } );

                return BadRequest ( new ResponseModelPadrao<Estoque> ( ) { IsSucces = false, Status = "Não foi possível excluir o estoque.", Data = null } );
            }
            catch (Exception ex)
            {
                return NotFound ( "Estoque não encontrado" );
            }

        }

        private async Task<ProdutoRequestModel> ValidaCampos(ProdutoRequestModel model) 
        {
            Estoque estoque = new Estoque();

            estoque = await _estoqueRepository.GetById(model.Id);

            if (string.IsNullOrEmpty(model.Name))
                model.Name = estoque.Name;

            return model;
        }

    }
}
