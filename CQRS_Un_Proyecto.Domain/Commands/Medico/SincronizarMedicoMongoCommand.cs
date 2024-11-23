using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Medico
{
    public class SincronizarMedicoMongoCommand : IRequest<Unit>
    {
        public MedicoEntity Medico { get; set; }

        public SincronizarMedicoMongoCommand(MedicoEntity medico)
        {
            Medico = medico;
        }
    }
}
