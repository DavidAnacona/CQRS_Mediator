using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Distribuidos.Infrastructure.Queries.HistorialEstadoConsulta;
using CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta;

namespace CQRS_Un_Proyecto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialEstadoConsultaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HistorialEstadoConsultaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<HistorialEstadoConsultaEntity>> GetAll()
        {
            return await _mediator.Send(new GetAllHistorialEstadoConsultaQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialEstadoConsultaEntity>> GetById(int id)
        {
            var query = new GetByIdHistorialEstadoConsultaQuery(id);
            var historial = await _mediator.Send(query);

            if (historial == null)
            {
                return NotFound();
            }

            return historial;
        }

        [HttpPost]
        public async Task<ActionResult<HistorialEstadoConsultaEntity>> Create(CrearHistorialEstadoConsultaCommand command)
        {
            var historial = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = historial.IdHistorialEstado }, historial);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ActualizarHistorialEstadoConsultaCommand command)
        {
            if (id != command.IdHistorialEstado)
            {
                return BadRequest();
            }

            var historial = await _mediator.Send(command);
            if (historial == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new EliminarHistorialEstadoConsultaCommand(id));

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
