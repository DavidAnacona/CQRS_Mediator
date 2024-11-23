using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Consulta
{
    public record ActualizarConsultaCommand(
        int IdConsulta,
        int IdPaciente,
        int IdMedico,
        int IdEstadoConsulta,
        string? Notas
    ) : IRequest<ConsultaEntity>;
}
