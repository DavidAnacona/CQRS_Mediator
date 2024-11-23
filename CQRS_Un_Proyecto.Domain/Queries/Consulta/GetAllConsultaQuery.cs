using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CQRS_Distribuidos.Infrastructure.Queries.Consulta
{
    public record GetAllConsultaQuery : IRequest<IEnumerable<ConsultaEntity>>;
}
