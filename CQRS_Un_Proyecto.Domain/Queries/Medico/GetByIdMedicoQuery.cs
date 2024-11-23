﻿using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Queries.Medico
{
    public record GetByIdMedicoQuery(int Id) : IRequest<MedicoEntity>;
}