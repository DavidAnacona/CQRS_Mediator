using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Distribuidos.Infrastructure.Queries.Consulta;
using CQRS_Distribuidos.Infrastructure.Commands.Consulta;
using CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta;

namespace CQRS_Un_Proyecto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsultaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Obtener todas las consultas
        [HttpGet]
        public async Task<IEnumerable<ConsultaEntity>> GetAll()
        {
            return await _mediator.Send(new GetAllConsultaQuery());
        }

        // Obtener consulta por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultaEntity>> GetById(int id)
        {
            var query = new GetByIdConsultaQuery(id);
            var consulta = await _mediator.Send(query);

            if (consulta == null)
            {
                return NotFound();
            }

            return consulta;
        }

        // Obtener consultas por estado
        [HttpGet("estado/{estadoId}")]
        public async Task<IEnumerable<ConsultaEntity>> GetByEstado(int estadoId)
        {
            return await _mediator.Send(new GetConsultaByEstadoQuery(estadoId));
        }

        // Crear una consulta
        [HttpPost]
        public async Task<ActionResult<ConsultaEntity>> Create(CrearConsultaCommand command)
        {
            // Crear la consulta
            var consulta = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = consulta.IdConsulta }, consulta);
        }

        // Actualizar una consulta
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ActualizarConsultaCommand command)
        {
            if (id != command.IdConsulta)
            {
                return BadRequest();
            }

            // Actualizar la consulta
            var consulta = await _mediator.Send(command);
            if (consulta == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Eliminar una consulta
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new EliminarConsultaCommand(id));

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
