using CQRS_Distribuidos.Infrastructure.Queries.HistorialEstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.HistorialEstadoConsulta
{
    public class GetAllHistorialEstadoConsultaHandler : IRequestHandler<GetAllHistorialEstadoConsultaQuery, IEnumerable<HistorialEstadoConsultaEntity>>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetAllHistorialEstadoConsultaHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<IEnumerable<HistorialEstadoConsultaEntity>> Handle(GetAllHistorialEstadoConsultaQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<HistorialEstadoConsultaEntity>("HistorialEstadoConsulta");

            var historiales = await collection.Find(Builders<HistorialEstadoConsultaEntity>.Filter.Empty)
                              .Project<HistorialEstadoConsultaEntity>(
                                  Builders<HistorialEstadoConsultaEntity>.Projection.Exclude("_id"))
                              .ToListAsync(cancellationToken);

            return historiales;
        }
    }
}
