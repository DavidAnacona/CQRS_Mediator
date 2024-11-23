using CQRS_Distribuidos.Infrastructure.Commands.Receta;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Receta
{
    public class EliminarRecetaHandler : IRequestHandler<EliminarRecetaCommand, bool>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public EliminarRecetaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<bool> Handle(EliminarRecetaCommand request, CancellationToken cancellationToken)
        {
            var recetaBuscada = await _dbContext.Receta.FindAsync(new object[] { request.Id }, cancellationToken);

            if (recetaBuscada == null)
            {
                return false;
            }

            _dbContext.Receta.Remove(recetaBuscada);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Send(new EliminarRecetaMongoCommand(request.Id), cancellationToken);

            return true;
        }
    }
}
