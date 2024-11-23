using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Paciente
{
    public record EliminarPacienteCommand (int Id) : IRequest<bool>;
}
