using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Paciente
{
    public class EliminarPacienteMongoCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public EliminarPacienteMongoCommand(int id)
        {
            Id = id;
        }
    }
}

