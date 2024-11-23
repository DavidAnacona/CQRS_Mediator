using CQRS_Distribuidos.Infrastructure.Commands.Paciente;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Paciente
{
    public class EliminarPacienteHandler : IRequestHandler<EliminarPacienteCommand, bool>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public EliminarPacienteHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<bool> Handle(EliminarPacienteCommand request, CancellationToken cancellationToken)
        {
            var PacienteBuscado = await _dbContext.Pacientes.FindAsync(new object[] { request.Id }, cancellationToken);

            if (PacienteBuscado == null)
            {
                return false;
            }

            _dbContext.Pacientes.Remove(PacienteBuscado);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Send(new EliminarPacienteMongoCommand(request.Id), cancellationToken);

            return true;
        }
    }
}
