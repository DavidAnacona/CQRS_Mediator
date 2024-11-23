﻿using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Receta
{
    public record ActualizarRecetaCommand(int IdReceta, int IdConsulta, string Medicamentos, string? Indicaciones) : IRequest<RecetaEntity>;
}