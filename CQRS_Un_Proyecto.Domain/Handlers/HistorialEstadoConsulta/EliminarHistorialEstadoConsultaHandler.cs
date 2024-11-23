using CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.HistorialEstadoConsulta
{
    public class EliminarHistorialEstadoConsultaHandler : IRequestHandler<EliminarHistorialEstadoConsultaCommand, bool>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public EliminarHistorialEstadoConsultaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<bool> Handle(EliminarHistorialEstadoConsultaCommand request, CancellationToken cancellationToken)
        {
            var historialBuscado = await _dbContext.HistorialEstadoConsulta.FindAsync(new object[] { request.Id }, cancellationToken);

            if (historialBuscado == null)
            {
                return false;
            }

            _dbContext.HistorialEstadoConsulta.Remove(historialBuscado);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Send(new EliminarHistorialEstadoConsultaMongoCommand(request.Id), cancellationToken);

            return true;
        }
    }
}
