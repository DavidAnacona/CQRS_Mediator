using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Medico
{
    public record CrearMedicoCommand(string Nombre, string Apellido, string Especialidad, string Telefono, string Correo) : IRequest<MedicoEntity>;
}
