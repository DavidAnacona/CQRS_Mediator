using CQRS_Distribuidos.Infrastructure.Queries.Receta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Receta
{
    public class GetByIdRecetaHandler : IRequestHandler<GetByIdRecetaQuery, RecetaEntity>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetByIdRecetaHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<RecetaEntity> Handle(GetByIdRecetaQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<RecetaEntity>("Receta");

            var filter = Builders<RecetaEntity>.Filter.Eq(r => r.IdReceta, request.Id);

            var projection = Builders<RecetaEntity>.Projection.Exclude("_id");

            var receta = await collection.Find(filter)
                              .Project<RecetaEntity>(projection)
                              .FirstOrDefaultAsync(cancellationToken);

            return receta;
        }
    }
}
