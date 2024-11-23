using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CQRS_Distribuidos.Infrastructure.Queries.Consulta
{
    public record GetConsultaByEstadoQuery(int IdEstadoConsulta) : IRequest<IEnumerable<ConsultaEntity>>;
}
