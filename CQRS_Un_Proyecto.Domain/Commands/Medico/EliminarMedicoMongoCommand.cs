using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Medico
{
    public class EliminarMedicoMongoCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public EliminarMedicoMongoCommand(int id)
        {
            Id = id;
        }
    }
}
