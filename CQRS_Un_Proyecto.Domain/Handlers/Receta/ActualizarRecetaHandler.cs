using CQRS_Distribuidos.Infrastructure.Commands.Receta;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Receta
{
    public class ActualizarRecetaHandler : IRequestHandler<ActualizarRecetaCommand, RecetaEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public ActualizarRecetaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<RecetaEntity> Handle(ActualizarRecetaCommand request, CancellationToken cancellationToken)
        {
            var recetaBuscada = await _dbContext.Receta.FindAsync(new object[] { request.IdReceta }, cancellationToken);

            if (recetaBuscada == null)
            {
                return null;
            }

            // Actualiza los campos en MySQL
            recetaBuscada.IdConsulta = request.IdConsulta;
            recetaBuscada.Medicamentos = request.Medicamentos;
            recetaBuscada.Indicaciones = request.Indicaciones;

            // Guarda los cambios en MySQL
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Crea el DTO para sincronizar con MongoDB
            var recetaDto = new RecetaEntity(recetaBuscada);

            // Envía el comando para sincronizar con MongoDB
            await _mediator.Send(new SincronizarRecetaMongoCommand(recetaDto), cancellationToken);

            return recetaDto;
        }
    }
}
