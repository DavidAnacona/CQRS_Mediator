using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Distribuidos.Infrastructure.Queries.Paciente;
using CQRS_Distribuidos.Infrastructure.Commands.Paciente;

namespace CQRS_Un_Proyecto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PacienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PacienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<PacienteEntity>> GetAlll()
        {
            return await _mediator.Send(new GetAllPacienteQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteEntity>> GetById(int id)
        {
            var query = new GetByIdPacienteQuery(id);
            var Paciente = await _mediator.Send(query);

            if (Paciente == null)
            {
                return NotFound();
            }

            return Paciente;
        }

        [HttpPost]
        public async Task<ActionResult<PacienteEntity>> Create(CrearPacienteCommand commando)
        {
            var Paciente = await _mediator.Send(commando);
            return CreatedAtAction(nameof(GetById), new { id = Paciente.IdPaciente }, Paciente);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ActualizarPacienteCommand command)
        {
            if (id != command.IdPaciente)
            {
                return BadRequest();
            }

            var Paciente = await _mediator.Send(command);
            if (Paciente == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new EliminarPacienteCommand(id));

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
