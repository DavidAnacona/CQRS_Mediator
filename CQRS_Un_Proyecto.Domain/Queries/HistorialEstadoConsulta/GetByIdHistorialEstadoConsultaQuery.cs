using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Queries.HistorialEstadoConsulta
{
    public record GetByIdHistorialEstadoConsultaQuery(int Id) : IRequest<HistorialEstadoConsultaEntity>;
}
