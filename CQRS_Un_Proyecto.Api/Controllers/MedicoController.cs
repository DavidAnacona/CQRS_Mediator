using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Distribuidos.Infrastructure.Queries.Medico;
using CQRS_Distribuidos.Infrastructure.Commands.Medico;

namespace CQRS_Un_Proyecto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<MedicoEntity>> GetAll()
        {
            return await _mediator.Send(new GetAllMedicoQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoEntity>> GetById(int id)
        {
            var query = new GetByIdMedicoQuery(id);
            var medico = await _mediator.Send(query);

            if (medico == null)
            {
                return NotFound();
            }

            return medico;
        }

        [HttpPost]
        public async Task<ActionResult<MedicoEntity>> Create(CrearMedicoCommand command)
        {
            var medico = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = medico.IdMedico }, medico);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ActualizarMedicoCommand command)
        {
            if (id != command.IdMedico)
            {
                return BadRequest();
            }

            var medico = await _mediator.Send(command);
            if (medico == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new EliminarMedicoCommand(id));

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
