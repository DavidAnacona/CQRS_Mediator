using System;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRS_Un_Proyecto.Domain.Entities
{
    public class MedicoEntity
    {
        public int IdMedico { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? Especialidad { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }

        public MedicoEntity()
        {
            this.IdMedico = 0;
            this.Nombre = String.Empty;
            this.Apellido = String.Empty;
            this.Especialidad = String.Empty;
            this.Telefono = String.Empty;
            this.Correo = String.Empty;
        }

        public MedicoEntity(Medico medico)
        {
            IdMedico = medico.IdMedico;
            Nombre = medico.Nombre;
            Apellido = medico.Apellido;
            Especialidad = medico?.Especialidad;
            Telefono = medico?.Telefono;
            Correo = medico?.Correo;
        }
    }
}
