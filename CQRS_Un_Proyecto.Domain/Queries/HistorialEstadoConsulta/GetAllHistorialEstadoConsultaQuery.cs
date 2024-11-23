using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CQRS_Distribuidos.Infrastructure.Queries.HistorialEstadoConsulta
{
    public record GetAllHistorialEstadoConsultaQuery : IRequest<IEnumerable<HistorialEstadoConsultaEntity>>;
}
