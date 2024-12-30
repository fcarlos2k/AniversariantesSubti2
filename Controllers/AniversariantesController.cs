using AniversariantesSubti.Models;
using AniversariantesSubti.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace AniversariantesSubti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AniversariantesController : ControllerBase
    {
        private readonly IAniversariantesRepository _repository;

        public AniversariantesController(IAniversariantesRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Aniversariante aniversariante)
        {
            _repository.Adicionar(aniversariante);
            return Ok($"Codigo do aniversariante INCLUIDO: {aniversariante.Id}");
        }

        [FeatureGate("FeaturePodeExcluir")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var aniversariante = _repository.ObterPorId(id);
            if (aniversariante is null)
                return BadRequest("Aniversariante nao encontrado");

                _repository.Remover(id);
                return Ok($" Codigo do Aniversariante DELETADO: {id}");

        }


        [HttpGet("ObterPorMes/{mes:int?}")]
        //public ActionResult<IEnumerable<Aniversariante>> Get(int? mes=DateTime.Now.Month)
        public ActionResult<IEnumerable<Aniversariante>> Get(int mes = 0)

        {
            if (mes == 0)
                mes = DateTime.Now.Month;
            
            if (mes < 1 || mes > 12)
                return NotFound("Mes incorreto");

            IEnumerable<Aniversariante> aniversariantes = _repository.ObterPorMes(mes);

            if (!aniversariantes.Any())
                return NotFound("Sem aniversariantes no mes");

            return Ok(aniversariantes);
        }

        [HttpGet("ObterPorNome")]
        public ActionResult<Aniversariante> ObterPorNome([FromQuery] string nome)
        {
            var aniversariantes = _repository.ObterPorNome(nome);

            if (!aniversariantes.Any())
                return NotFound("Aniversariante nao encontrado");

            return Ok(aniversariantes);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Aniversariante>> ListarAniversariantesProximo7dias()
        {
            IEnumerable<Aniversariante> Aniversariantes = _repository.ObterTodosProximo7dias();

            if (!Aniversariantes.Any())
                return NotFound("Sem aniversariantes para os proximos 7 dias");

            return Ok(Aniversariantes);
        }
    }
}
