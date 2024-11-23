using CQRS_Distribuidos.Infrastructure.Commands.Paciente;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;

namespace CQRS_Distribuidos.Application.Handlers.Paciente
{
    public class CrearPacienteHandler : IRequestHandler<CrearPacienteCommand, PacienteEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public CrearPacienteHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task<PacienteEntity> Handle(CrearPacienteCommand request, CancellationToken cancellationToken)
        {
            CQRS_Un_Proyecto.Infrastructure.Model.Paciente paciente = new()
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = request.FechaNacimiento,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                Correo = request.Correo,
            };

            _dbContext.Pacientes.Add(paciente);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var pacienteDto = new PacienteEntity
            {
                IdPaciente = paciente.IdPaciente,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                FechaNacimiento = paciente.FechaNacimiento,
                Direccion = paciente.Direccion,
                Telefono = paciente.Telefono,
                Correo = paciente.Correo
            };

            await _mediator.Send(new SincronizarPacienteMongoCommand(pacienteDto), cancellationToken);

            return pacienteDto;
        }
    }
}
