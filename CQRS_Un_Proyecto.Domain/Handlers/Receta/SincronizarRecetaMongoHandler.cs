using CQRS_Distribuidos.Infrastructure.Commands.Receta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Receta
{
    public class SincronizarRecetaMongoHandler : IRequestHandler<SincronizarRecetaMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public SincronizarRecetaMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(SincronizarRecetaMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<RecetaEntity>("Receta");

            // Define el filtro y las actualizaciones para MongoDB
            var filter = Builders<RecetaEntity>.Filter.Eq(r => r.IdReceta, request.Receta.IdReceta);
            var update = Builders<RecetaEntity>.Update
                .Set(r => r.IdConsulta, request.Receta.IdConsulta)
                .Set(r => r.Medicamentos, request.Receta.Medicamentos)
                .Set(r => r.Indicaciones, request.Receta.Indicaciones);

            // Ejecuta la operación de actualización en MongoDB
            await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);

            return Unit.Value;
        }
    }
}
