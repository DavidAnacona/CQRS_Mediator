using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Consulta
{
    public class EliminarConsultaMongoCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public EliminarConsultaMongoCommand(int id)
        {
            Id = id;
        }
    }
}
