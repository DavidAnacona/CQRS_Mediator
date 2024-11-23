using CQRS_Distribuidos.Infrastructure.Commands.Consulta;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Consulta
{
    public class EliminarConsultaHandler : IRequestHandler<EliminarConsultaCommand, bool>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public EliminarConsultaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<bool> Handle(EliminarConsultaCommand request, CancellationToken cancellationToken)
        {
            var consulta = await _dbContext.Consulta.FindAsync(new object[] { request.Id }, cancellationToken);

            if (consulta == null)
            {
                return false;
            }

            _dbContext.Consulta.Remove(consulta);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Send(new EliminarConsultaMongoCommand(request.Id), cancellationToken);

            return true;
        }
    }
}
