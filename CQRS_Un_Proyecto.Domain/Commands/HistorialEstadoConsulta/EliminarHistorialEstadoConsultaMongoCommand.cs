using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta
{
    public class EliminarHistorialEstadoConsultaMongoCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public EliminarHistorialEstadoConsultaMongoCommand(int id)
        {
            Id = id;
        }
    }
}
