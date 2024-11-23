using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.EstadoConsulta
{
    public class EliminarEstadoConsultaMongoCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public EliminarEstadoConsultaMongoCommand(int id)
        {
            Id = id;
        }
    }
}
