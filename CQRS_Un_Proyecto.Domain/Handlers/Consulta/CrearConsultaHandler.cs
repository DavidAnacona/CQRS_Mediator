using CQRS_Distribuidos.Infrastructure.Commands.Consulta;
using CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Consulta
{
    public class CrearConsultaHandler : IRequestHandler<CrearConsultaCommand, ConsultaEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public CrearConsultaHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<ConsultaEntity> Handle(CrearConsultaCommand request, CancellationToken cancellationToken)
        {
            var consulta = new Consultum
            {
                IdPaciente = request.IdPaciente,
                IdMedico = request.IdMedico,
                Notas = request.Notas,
                IdEstadoConsulta = request.IdEstadoConsulta,
            };

            _dbContext.Consulta.Add(consulta);
            await _dbContext.SaveChangesAsync(cancellationToken);

            Consultum consultaCreada = await _dbContext.Consulta
                .Include(x => x.IdPacienteNavigation)
                .Include(x => x.IdMedicoNavigation)
                .Include(x => x.IdEstadoConsultaNavigation)
                .Where(x => x.IdConsulta == consulta.IdConsulta)
                .FirstOrDefaultAsync();

            var historial = new HistorialEstadoConsultum
            {
                IdConsulta = consulta.IdConsulta,
                IdEstadoConsulta = consulta.IdEstadoConsulta,
                FechaCambio = DateTime.UtcNow,
                UsuarioResponsable = "Sistema",
                Comentario = "Consulta creada."
            };
            _dbContext.HistorialEstadoConsulta.Add(historial);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var consultaEntity = new ConsultaEntity(consultaCreada);
            var historialEntity = new HistorialEstadoConsultaEntity(historial);
            // Sincronizar con MongoDB
            await _mediator.Send(new SincronizarConsultaMongoCommand(consultaEntity), cancellationToken);
            await _mediator.Send(new SincronizarHistorialEstadoConsultaMongoCommand(historialEntity), cancellationToken);

            return consultaEntity;
        }
    }
}
