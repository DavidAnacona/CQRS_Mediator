using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Consulta
{
    public class SincronizarConsultaMongoCommand : IRequest<Unit>
    {
        public ConsultaEntity Consulta { get; set; }

        public SincronizarConsultaMongoCommand(ConsultaEntity consulta)
        {
            Consulta = consulta;
        }
    }
}
