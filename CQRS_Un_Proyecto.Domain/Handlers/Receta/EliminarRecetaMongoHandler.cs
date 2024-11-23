using CQRS_Distribuidos.Infrastructure.Commands.Receta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Receta
{
    public class EliminarRecetaMongoHandler : IRequestHandler<EliminarRecetaMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public EliminarRecetaMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(EliminarRecetaMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<RecetaEntity>("Receta");
            var filter = Builders<RecetaEntity>.Filter.Eq(r => r.IdReceta, request.Id);

            await collection.DeleteOneAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
