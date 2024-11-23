using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Queries.Consulta
{
    public record GetByIdConsultaQuery(int Id) : IRequest<ConsultaEntity>;
}
