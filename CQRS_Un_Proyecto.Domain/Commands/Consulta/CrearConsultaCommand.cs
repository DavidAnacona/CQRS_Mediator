using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Consulta
{
    public record CrearConsultaCommand(int IdPaciente, DateTime FechaHora, int IdEstadoConsulta, int IdMedico, string? Notas) : IRequest<ConsultaEntity>;
}
