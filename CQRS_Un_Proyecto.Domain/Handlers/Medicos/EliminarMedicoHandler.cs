using CQRS_Distribuidos.Infrastructure.Commands.Medico;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Medico
{
    public class EliminarMedicoHandler : IRequestHandler<EliminarMedicoCommand, bool>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public EliminarMedicoHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<bool> Handle(EliminarMedicoCommand request, CancellationToken cancellationToken)
        {
            var MedicoBuscado = await _dbContext.Medicos.FindAsync(new object[] { request.Id }, cancellationToken);

            if (MedicoBuscado == null)
            {
                return false;
            }

            _dbContext.Medicos.Remove(MedicoBuscado);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Send(new EliminarMedicoMongoCommand(request.Id), cancellationToken);

            return true;
        }
    }
}
