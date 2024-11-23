using CQRS_Distribuidos.Infrastructure.Queries.Receta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Receta
{
    public class GetAllRecetaHandler : IRequestHandler<GetAllRecetaQuery, IEnumerable<RecetaEntity>>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetAllRecetaHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<IEnumerable<RecetaEntity>> Handle(GetAllRecetaQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<RecetaEntity>("Receta");

            var recetas = await collection.Find(Builders<RecetaEntity>.Filter.Empty)
                              .Project<RecetaEntity>(
                                  Builders<RecetaEntity>.Projection.Exclude("_id"))
                              .ToListAsync(cancellationToken);

            return recetas;
        }
    }
}
