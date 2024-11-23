using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Paciente
{
    public record CrearPacienteCommand(int IdPaciente, string Nombre, string Apellido, DateTime FechaNacimiento, string Telefono, string Correo, string Direccion) : IRequest<PacienteEntity>;
}
