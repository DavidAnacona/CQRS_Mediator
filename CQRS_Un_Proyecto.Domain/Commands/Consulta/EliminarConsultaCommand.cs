using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Consulta
{
    public record EliminarConsultaCommand(int Id) : IRequest<bool>;
}
