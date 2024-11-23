using CQRS_Distribuidos.Infrastructure.Commands.EstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.EstadoConsulta
{
    public class CrearEstadoConsultaHandler : IRequestHandler<CrearEstadoConsultaCommand, EstadoConsultaEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public CrearEstadoConsultaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<EstadoConsultaEntity> Handle(CrearEstadoConsultaCommand request, CancellationToken cancellationToken)
        {
            var estadoConsultum = new EstadoConsultum
            {
                NombreEstado = request.NombreEstado
            };

            _dbContext.EstadoConsulta.Add(estadoConsultum);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var estadoDto = new EstadoConsultaEntity(estadoConsultum);

            await _mediator.Send(new SincronizarEstadoConsultaMongoCommand(estadoDto), cancellationToken);

            return estadoDto;
        }
    }
}
