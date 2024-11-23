using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRS_Un_Proyecto.Domain.Entities
{
    public class PacienteEntity
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }

        public PacienteEntity()
        {
            this.IdPaciente = 0;
            this.Nombre = String.Empty;
            this.Apellido = String.Empty;
            this.FechaNacimiento = DateTime.Now;
            this.Telefono = String.Empty;
            this.Correo = String.Empty;
            this.Direccion = String.Empty;
        }

        public PacienteEntity(Paciente paciente)
        {
            IdPaciente = paciente.IdPaciente;
            Nombre = paciente.Nombre;
            Apellido = paciente.Apellido;
            FechaNacimiento = paciente.FechaNacimiento;
            Telefono = paciente?.Telefono;
            Correo = paciente?.Correo;
            Direccion = paciente?.Direccion;
        }
    }
}
