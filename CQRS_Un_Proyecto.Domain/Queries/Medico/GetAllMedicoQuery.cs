using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Queries.Medico
{
    public record GetAllMedicoQuery : IRequest<IEnumerable<MedicoEntity>>;
}
