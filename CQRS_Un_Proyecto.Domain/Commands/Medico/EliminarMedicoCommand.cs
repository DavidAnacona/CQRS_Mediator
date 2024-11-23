using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Medico
{
    public record EliminarMedicoCommand(int Id) : IRequest<bool>;
}
