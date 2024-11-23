using CQRS_Distribuidos.Infrastructure.Commands.EstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.EstadoConsulta
{
    public class ActualizarEstadoConsultaHandler : IRequestHandler<ActualizarEstadoConsultaCommand, EstadoConsultaEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public ActualizarEstadoConsultaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<EstadoConsultaEntity> Handle(ActualizarEstadoConsultaCommand request, CancellationToken cancellationToken)
        {
            var estadoBuscado = await _dbContext.EstadoConsulta.FindAsync(new object[] { request.IdEstadoConsulta }, cancellationToken);

            if (estadoBuscado == null)
            {
                return null;
            }

            // Actualiza los campos en MySQL
            estadoBuscado.NombreEstado = request.NombreEstado;

            // Guarda los cambios en MySQL
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Crea el DTO para sincronizar con MongoDB
            var estadoDto = new EstadoConsultaEntity(estadoBuscado);

            // Envia el comando para sincronizar con MongoDB
            await _mediator.Send(new SincronizarEstadoConsultaMongoCommand(estadoDto), cancellationToken);

            return estadoDto;
        }
    }
}
