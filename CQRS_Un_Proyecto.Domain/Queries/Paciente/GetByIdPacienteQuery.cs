using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Queries.Paciente
{
    public record GetByIdPacienteQuery(int Id) : IRequest<PacienteEntity>;
}
