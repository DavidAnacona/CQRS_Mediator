using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Distribuidos.Infrastructure.Queries.EstadoConsulta;
using CQRS_Distribuidos.Infrastructure.Commands.EstadoConsulta;

namespace CQRS_Un_Proyecto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoConsultaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EstadoConsultaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<EstadoConsultaEntity>> GetAll()
        {
            return await _mediator.Send(new GetAllEstadoConsultaQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoConsultaEntity>> GetById(int id)
        {
            var query = new GetByIdEstadoConsultaQuery(id);
            var estadoConsulta = await _mediator.Send(query);

            if (estadoConsulta == null)
            {
                return NotFound();
            }

            return estadoConsulta;
        }

        [HttpPost]
        public async Task<ActionResult<EstadoConsultaEntity>> Create(CrearEstadoConsultaCommand command)
        {
            var estadoConsulta = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = estadoConsulta.IdEstadoConsulta }, estadoConsulta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ActualizarEstadoConsultaCommand command)
        {
            if (id != command.IdEstadoConsulta)
            {
                return BadRequest();
            }

            var estadoConsulta = await _mediator.Send(command);
            if (estadoConsulta == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new EliminarEstadoConsultaCommand(id));

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
