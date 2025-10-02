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
    public class ReportesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ventas")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<decimal>> GetVentasPorFecha([FromQuery] DateTime fecha)
        {
            var query = new ObtenerTotalVentasQuery { Fecha = fecha };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
