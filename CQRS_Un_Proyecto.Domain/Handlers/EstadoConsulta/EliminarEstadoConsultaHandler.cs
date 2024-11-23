using CQRS_Distribuidos.Infrastructure.Commands.EstadoConsulta;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.EstadoConsulta
{
    public class EliminarEstadoConsultaHandler : IRequestHandler<EliminarEstadoConsultaCommand, bool>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public EliminarEstadoConsultaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<bool> Handle(EliminarEstadoConsultaCommand request, CancellationToken cancellationToken)
        {
            var estadoBuscado = await _dbContext.EstadoConsulta.FindAsync(new object[] { request.Id }, cancellationToken);

            if (estadoBuscado == null)
            {
                return false;
            }

            _dbContext.EstadoConsulta.Remove(estadoBuscado);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Send(new EliminarEstadoConsultaMongoCommand(request.Id), cancellationToken);

            return true;
        }
    }
}
