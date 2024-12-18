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
        public ActionResult Post([FromBody] Aniversariantes aniversariantes)
        {
            _repository.Adicionar(aniversariantes);
            return Ok("Codigo do aniversariante INCLUIDO: " + aniversariantes.Id);
        }

        [FeatureGate("FeaturePodeExcluir")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var aniversariante = _repository.ObterPorId(id);
            if (aniversariante is null)
            {
                return BadRequest("Aniversariante nao encontrado");
            }
            else
            {
                _repository.Remover(id);
                return Ok(" Codigo do Aniversariante DELETADO: " + id);
            }
        }

        [HttpGet("ObterPorMes/{mes:int?}")]
        //int mesatual = DateTime.Now.Month;
        //public ActionResult<Aniversariantes> Get(int mes = 01)
        public ActionResult<Aniversariantes> Get(int mes = 0)
        {
            if (mes == 0)
            {
                mes = DateTime.Now.Month;
                //mes = 03;
            }

            IEnumerable<Aniversariantes> aniversariantes = _repository.ObterPorMes(mes);

            if (mes <1 || mes > 12)
            {
                return NotFound("Mes incorreto");
            }

            if (aniversariantes == null)
            {
                return NotFound("Sem aniversariantes no mes");
            }

            return Ok(aniversariantes);
        }

        [HttpGet("ObterPorNome")]
        public ActionResult<Aniversariantes> ObterPorNome([FromQuery] string nome)
        {
            var Aniversariantes = _repository.ObterPorNome(nome);
            if (Aniversariantes == null)
            {
                return NotFound("Aniversariante nao encontrado");
            }
            return Ok(Aniversariantes);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Aniversariantes>> ListarAniversariantesProximo7dias()
        {
            IEnumerable<Aniversariantes> aniversariantes = _repository.ObterTodosProximo7dias();

            if (aniversariantes == null)
            {
                return NotFound("Sem aniversariantes para os proximos 7 dias");
            }
            return Ok(aniversariantes);
        }
    }
}
