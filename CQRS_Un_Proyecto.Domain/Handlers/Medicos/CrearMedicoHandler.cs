using CQRS_Distribuidos.Infrastructure.Commands.Medico;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Medico
{
    public class CrearMedicoHandler : IRequestHandler<CrearMedicoCommand, MedicoEntity>
    {
        private readonly GestionSaludContext _dbContext;
        private readonly IMediator _mediator;

        public CrearMedicoHandler(GestionSaludContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<MedicoEntity> Handle(CrearMedicoCommand request, CancellationToken cancellationToken)
        {
            CQRS_Un_Proyecto.Infrastructure.Model.Medico medico = new()
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Especialidad = request.Especialidad,
                Telefono = request.Telefono,
                Correo = request.Correo
            };

            _dbContext.Medicos.Add(medico);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var medicoDto = new MedicoEntity
            {
                IdMedico = medico.IdMedico,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Especialidad = medico.Especialidad,
                Telefono = medico.Telefono,
                Correo = medico.Correo
            };

            await _mediator.Send(new SincronizarMedicoMongoCommand(medicoDto), cancellationToken);

            return medicoDto;
        }
    }
}
