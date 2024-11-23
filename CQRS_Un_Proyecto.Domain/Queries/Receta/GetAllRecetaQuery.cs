using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CQRS_Distribuidos.Infrastructure.Queries.Receta
{
    public record GetAllRecetaQuery : IRequest<IEnumerable<RecetaEntity>>;
}
