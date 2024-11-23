using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Queries.EstadoConsulta
{
    public record GetByIdEstadoConsultaQuery(int Id) : IRequest<EstadoConsultaEntity>;
}
