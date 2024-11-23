using CQRS_Distribuidos.Infrastructure.Commands.Paciente;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Paciente
{
    public class ActualizarPacienteHandler : IRequestHandler<ActualizarPacienteCommand, PacienteEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public ActualizarPacienteHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<PacienteEntity> Handle(ActualizarPacienteCommand request, CancellationToken cancellationToken)
        {
            var PacienteBuscado = await _dbContext.Pacientes.FindAsync(new object[] { request.IdPaciente }, cancellationToken);

            if (PacienteBuscado == null)
            {
                return null;
            }

            // Actualiza los campos en MySQL
            PacienteBuscado.Nombre = request.Nombre;
            PacienteBuscado.Apellido = request.Apellido;
            PacienteBuscado.FechaNacimiento = request.FechaNacimiento;
            PacienteBuscado.Direccion = request.Direccion;
            PacienteBuscado.Telefono = request.Telefono;
            PacienteBuscado.Correo = request.Correo;

            // Guarda los cambios en MySQL
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Crea el DTO para sincronizar con MongoDB
            var pacienteDto = new PacienteEntity
            {
                IdPaciente = PacienteBuscado.IdPaciente,
                Nombre = PacienteBuscado.Nombre,
                Apellido = PacienteBuscado.Apellido,
                FechaNacimiento = PacienteBuscado.FechaNacimiento,
                Direccion = PacienteBuscado.Direccion,
                Telefono = PacienteBuscado.Telefono,
                Correo = PacienteBuscado.Correo,
            };

            // Envia el comando para sincronizar con MongoDB
            await _mediator.Send(new SincronizarPacienteMongoCommand(pacienteDto), cancellationToken);

            return pacienteDto;
        }
    }
}
