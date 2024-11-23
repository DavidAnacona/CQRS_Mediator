using CQRS_Distribuidos.Infrastructure.Commands.EstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.EstadoConsulta
{
    public class SincronizarEstadoConsultaMongoHandler : IRequestHandler<SincronizarEstadoConsultaMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public SincronizarEstadoConsultaMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(SincronizarEstadoConsultaMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<EstadoConsultaEntity>("EstadoConsulta");

            // Define el filtro y las actualizaciones para MongoDB
            var filter = Builders<EstadoConsultaEntity>.Filter.Eq(e => e.IdEstadoConsulta, request.EstadoConsulta.IdEstadoConsulta);
            var update = Builders<EstadoConsultaEntity>.Update
                .Set(e => e.NombreEstado, request.EstadoConsulta.NombreEstado);

            // Ejecuta la operación de actualización en MongoDB
            await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);

            return Unit.Value;
        }
    }
}
