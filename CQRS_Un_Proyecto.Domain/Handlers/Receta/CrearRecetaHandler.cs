using CQRS_Distribuidos.Infrastructure.Commands.Receta;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Receta
{
    public class CrearRecetaHandler : IRequestHandler<CrearRecetaCommand, RecetaEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public CrearRecetaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<RecetaEntity> Handle(CrearRecetaCommand request, CancellationToken cancellationToken)
        {
            var receta = new Recetum
            {
                IdConsulta = request.IdConsulta,
                Medicamentos = request.Medicamentos,
                Indicaciones = request.Indicaciones
            };

            _dbContext.Receta.Add(receta);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var recetaDto = new RecetaEntity(receta);

            await _mediator.Send(new SincronizarRecetaMongoCommand(recetaDto), cancellationToken);

            return recetaDto;
        }
    }
}
