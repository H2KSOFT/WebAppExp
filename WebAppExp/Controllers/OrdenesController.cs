using clApplication.Commands;
using clApplication.DTOs;
using clApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppExp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdenesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdenesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrdenDto>>> Get()
        {
            var query = new ObtenerOrdenesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenDto>> GetById(int id)
        {
            var query = new ObtenerOrdenPorIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Vendedor")]
        public async Task<ActionResult<int>> Create([FromBody] CrearOrdenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(int id, [FromBody] ActualizarOrdenCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new EliminarOrdenCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("cliente/{cliente}")]
        public async Task<ActionResult<List<OrdenDto>>> GetByCliente(string cliente)
        {
            var query = new ObtenerOrdenesQuery { Cliente = cliente };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
