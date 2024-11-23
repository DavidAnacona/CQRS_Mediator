using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Paciente
{
    public class SincronizarPacienteMongoCommand : IRequest<Unit> 
    {
        public PacienteEntity Paciente { get; set; }

        public SincronizarPacienteMongoCommand(PacienteEntity paciente)
        {
            Paciente = paciente;
        }
    }
}
