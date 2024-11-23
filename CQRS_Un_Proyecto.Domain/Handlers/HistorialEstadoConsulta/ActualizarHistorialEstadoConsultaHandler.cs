using CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.HistorialEstadoConsulta
{
    public class ActualizarHistorialEstadoConsultaHandler : IRequestHandler<ActualizarHistorialEstadoConsultaCommand, HistorialEstadoConsultaEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public ActualizarHistorialEstadoConsultaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<HistorialEstadoConsultaEntity> Handle(ActualizarHistorialEstadoConsultaCommand request, CancellationToken cancellationToken)
        {
            var historialBuscado = await _dbContext.HistorialEstadoConsulta.FindAsync(new object[] { request.IdHistorialEstado }, cancellationToken);

            if (historialBuscado == null)
            {
                return null;
            }

            // Actualiza los campos en MySQL
            historialBuscado.IdConsulta = request.IdConsulta;
            historialBuscado.IdEstadoConsulta = request.IdEstadoConsulta;
            historialBuscado.FechaCambio = request.FechaCambio;
            historialBuscado.UsuarioResponsable = request.UsuarioResponsable;
            historialBuscado.Comentario = request.Comentario;

            // Guarda los cambios en MySQL
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Crea el DTO para sincronizar con MongoDB
            var historialDto = new HistorialEstadoConsultaEntity(historialBuscado);

            // Envía el comando para sincronizar con MongoDB
            await _mediator.Send(new SincronizarHistorialEstadoConsultaMongoCommand(historialDto), cancellationToken);

            return historialDto;
        }
    }
}
