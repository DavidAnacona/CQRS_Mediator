using CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.HistorialEstadoConsulta
{
    public class CrearHistorialEstadoConsultaHandler : IRequestHandler<CrearHistorialEstadoConsultaCommand, HistorialEstadoConsultaEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public CrearHistorialEstadoConsultaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<HistorialEstadoConsultaEntity> Handle(CrearHistorialEstadoConsultaCommand request, CancellationToken cancellationToken)
        {
            var historial = new HistorialEstadoConsultum
            {
                IdConsulta = request.IdConsulta,
                IdEstadoConsulta = request.IdEstadoConsulta,
                FechaCambio = request.FechaCambio,
                UsuarioResponsable = request.UsuarioResponsable,
                Comentario = request.Comentario
            };

            _dbContext.HistorialEstadoConsulta.Add(historial);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var historialDto = new HistorialEstadoConsultaEntity(historial);

            await _mediator.Send(new SincronizarHistorialEstadoConsultaMongoCommand(historialDto), cancellationToken);

            return historialDto;
        }
    }
}
