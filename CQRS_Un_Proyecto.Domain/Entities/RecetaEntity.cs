using System;
using CQRS_Un_Proyecto.Infrastructure.Model;

namespace CQRS_Un_Proyecto.Domain.Entities
{
    public class RecetaEntity
    {
        public int IdReceta { get; set; }

        public int IdConsulta { get; set; }

        public string Medicamentos { get; set; } = null!;

        public string? Indicaciones { get; set; }
        public RecetaEntity()
        {
        }

        public RecetaEntity(Recetum receta)
        {
            IdReceta = receta.IdReceta;
            IdConsulta = receta.IdConsulta;
            Medicamentos = receta.Medicamentos;
            Indicaciones = receta.Indicaciones;
        }
    }
}
