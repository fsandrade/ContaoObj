using AutoMapper;
using ContaObj.Application.Interfaces;
using ContaObj.Domain.Exceptions;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel.Transacao;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContaObj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacaoManager manager;
        private readonly IMapper mapper;

        public TransacoesController(ITransacaoManager manager, IMapper mapper)
        {
            this.manager = manager;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transacao>>> GetAsync()
        {
            return Ok(await manager.ConsultaTransacoesAsync());
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [ProducesResponseType(typeof(TransacaoViewModel), StatusCodes.Status202Accepted)]
        public async Task<ActionResult<TransacaoViewModel>> Post(NovaTransacao transacao)
        {
            Transacao transacaoAceita;
            try
            {
                transacaoAceita = await manager.TranferirAsync(mapper.Map<Transacao>(transacao));
            }
            catch (TransacaoInvalidaException ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
            return Accepted(mapper.Map<TransacaoViewModel>(transacaoAceita));
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}