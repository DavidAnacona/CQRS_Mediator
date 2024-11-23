using CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.HistorialEstadoConsulta
{
    public class EliminarHistorialEstadoConsultaMongoHandler : IRequestHandler<EliminarHistorialEstadoConsultaMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public EliminarHistorialEstadoConsultaMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(EliminarHistorialEstadoConsultaMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<HistorialEstadoConsultaEntity>("HistorialEstadoConsulta");
            var filter = Builders<HistorialEstadoConsultaEntity>.Filter.Eq(h => h.IdHistorialEstado, request.Id);

            await collection.DeleteOneAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
