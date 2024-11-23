using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Distribuidos.Infrastructure.Queries.Receta;
using CQRS_Distribuidos.Infrastructure.Commands.Receta;

namespace CQRS_Un_Proyecto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecetaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<RecetaEntity>> GetAll()
        {
            return await _mediator.Send(new GetAllRecetaQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecetaEntity>> GetById(int id)
        {
            var query = new GetByIdRecetaQuery(id);
            var receta = await _mediator.Send(query);

            if (receta == null)
            {
                return NotFound();
            }

            return receta;
        }

        [HttpPost]
        public async Task<ActionResult<RecetaEntity>> Create(CrearRecetaCommand command)
        {
            var receta = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = receta.IdReceta }, receta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ActualizarRecetaCommand command)
        {
            if (id != command.IdReceta)
            {
                return BadRequest();
            }

            var receta = await _mediator.Send(command);
            if (receta == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new EliminarRecetaCommand(id));

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

