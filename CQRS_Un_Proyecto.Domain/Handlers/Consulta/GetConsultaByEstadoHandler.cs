using CQRS_Distribuidos.Infrastructure.Queries.Consulta;
using CQRS_Un_Proyecto.Domain.Entities;
using CQRS_Un_Proyecto.Infrastructure.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Consulta
{
    public class GetConsultaByEstadoHandler : IRequestHandler<GetConsultaByEstadoQuery, IEnumerable<ConsultaEntity>>
    {
        private readonly GestionSaludContext _dbContext;

        public GetConsultaByEstadoHandler(GestionSaludContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ConsultaEntity>> Handle(GetConsultaByEstadoQuery request, CancellationToken cancellationToken)
        {
            var consultas = await _dbContext.Consulta
                .Where(c => c.IdEstadoConsulta == request.IdEstadoConsulta)
                .ToListAsync(cancellationToken);

            return consultas.Select(c => new ConsultaEntity(c));
        }
    }
}
