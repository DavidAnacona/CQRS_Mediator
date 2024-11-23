using CQRS_Distribuidos.Infrastructure.Queries.HistorialEstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.HistorialEstadoConsulta
{
    public class GetByIdHistorialEstadoConsultaHandler : IRequestHandler<GetByIdHistorialEstadoConsultaQuery, HistorialEstadoConsultaEntity>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetByIdHistorialEstadoConsultaHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<HistorialEstadoConsultaEntity> Handle(GetByIdHistorialEstadoConsultaQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<HistorialEstadoConsultaEntity>("HistorialEstadoConsulta");

            var filter = Builders<HistorialEstadoConsultaEntity>.Filter.Eq(h => h.IdHistorialEstado, request.Id);

            var projection = Builders<HistorialEstadoConsultaEntity>.Projection.Exclude("_id");

            var historial = await collection.Find(filter)
                              .Project<HistorialEstadoConsultaEntity>(projection)
                              .FirstOrDefaultAsync(cancellationToken);

            return historial;
        }
    }
}
