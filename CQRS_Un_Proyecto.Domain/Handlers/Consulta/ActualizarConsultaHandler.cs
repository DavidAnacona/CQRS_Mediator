using CQRS_Distribuidos.Infrastructure.Commands.Consulta;
using CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;

namespace CQRS_Distribuidos.Application.Handlers.Consulta
{
    public class ActualizarConsultaHandler : IRequestHandler<ActualizarConsultaCommand, ConsultaEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public ActualizarConsultaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<ConsultaEntity> Handle(ActualizarConsultaCommand request, CancellationToken cancellationToken)
        {
            var consulta = await _dbContext.Consulta.FindAsync(new object[] { request.IdConsulta }, cancellationToken);

            if (consulta == null)
            {
                return null;
            }

            // Actualiza la consulta
            consulta.IdPaciente = request.IdPaciente;
            consulta.IdMedico = request.IdMedico;
            consulta.IdEstadoConsulta = request.IdEstadoConsulta;
            consulta.Notas = request.Notas;

            await _dbContext.SaveChangesAsync(cancellationToken);

            var historial = new HistorialEstadoConsultum
            {
                IdConsulta = consulta.IdConsulta,
                IdEstadoConsulta = consulta.IdEstadoConsulta,
                FechaCambio = DateTime.UtcNow,
                UsuarioResponsable = "Sistema",
                Comentario = "Consulta actualizada automáticamente."
            };
            _dbContext.HistorialEstadoConsulta.Add(historial);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var consultaEntity = new ConsultaEntity(consulta);
            var historialEntity = new HistorialEstadoConsultaEntity(historial);
            
            await _mediator.Send(new SincronizarConsultaMongoCommand(consultaEntity), cancellationToken);
            await _mediator.Send(new SincronizarHistorialEstadoConsultaMongoCommand(historialEntity), cancellationToken);

            return consultaEntity;
        }
    }
}
