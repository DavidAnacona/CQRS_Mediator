using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Queries.Receta
{
    public record GetByIdRecetaQuery(int Id) : IRequest<RecetaEntity>;
}
