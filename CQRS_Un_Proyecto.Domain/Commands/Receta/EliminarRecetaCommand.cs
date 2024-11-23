using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Receta
{
    public record EliminarRecetaCommand(int Id) : IRequest<bool>;
}
