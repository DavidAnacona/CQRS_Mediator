using System;
using System.Collections.Generic;
using CQRS_Un_Proyecto.Infrastructure.Model;

namespace CQRS_Un_Proyecto.Domain.Entities
{
    public class ConsultaEntity
    {
        public int IdConsulta { get; set; }
        public DateTime FechaHora { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public int IdEstadoConsulta { get; set; }
        public string? Notas { get; set; }
        public string? NombrePaciente { get; set; }
        public string? NombreMedico { get; set; }
        public string? NombreEstadoConsulta { get; set; }

        public ConsultaEntity()
        {
        }

        public ConsultaEntity(Consultum consulta)
        {
            IdConsulta = consulta.IdConsulta;
            FechaHora = consulta.FechaHora;
            IdPaciente = consulta.IdPaciente;
            IdMedico = consulta.IdMedico;
            IdEstadoConsulta = consulta.IdEstadoConsulta;
            Notas = consulta.Notas;
            if(consulta.IdEstadoConsultaNavigation != null)
            {
                NombreEstadoConsulta = consulta.IdEstadoConsultaNavigation.NombreEstado;
            }
            if (consulta.IdPacienteNavigation != null)
            {
                NombrePaciente = consulta.IdPacienteNavigation.Nombre + " " + consulta.IdPacienteNavigation.Apellido;
            }
            if (consulta.IdMedicoNavigation != null)
            {
                NombreMedico = consulta.IdMedicoNavigation.Nombre;
            }
        }
    }
}