using CQRS_Distribuidos.Infrastructure.Commands.Medico;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Medico
{
    public class ActualizarMedicoHandler : IRequestHandler<ActualizarMedicoCommand, MedicoEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public ActualizarMedicoHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<MedicoEntity> Handle(ActualizarMedicoCommand request, CancellationToken cancellationToken)
        {
            var MedicoBuscado = await _dbContext.Medicos.FindAsync(new object[] { request.IdMedico }, cancellationToken);

            if (MedicoBuscado == null)
            {
                return null;
            }

            // Actualiza los campos en MySQL
            MedicoBuscado.Nombre = request.Nombre;
            MedicoBuscado.Apellido = request.Apellido;
            MedicoBuscado.Especialidad = request.Especialidad;
            MedicoBuscado.Telefono = request.Telefono;
            MedicoBuscado.Correo = request.Correo;

            // Guarda los cambios en MySQL
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Crea el DTO para sincronizar con MongoDB
            var medicoDto = new MedicoEntity
            {
                IdMedico = MedicoBuscado.IdMedico,
                Nombre = MedicoBuscado.Nombre,
                Apellido = MedicoBuscado.Apellido,
                Especialidad = MedicoBuscado.Especialidad,
                Telefono = MedicoBuscado.Telefono,
                Correo = MedicoBuscado.Correo
            };

            // Envia el comando para sincronizar con MongoDB
            await _mediator.Send(new SincronizarMedicoMongoCommand(medicoDto), cancellationToken);

            return medicoDto;
        }
    }
}
