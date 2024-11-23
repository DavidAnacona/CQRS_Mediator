using CQRS_Distribuidos.Infrastructure.Queries.Medico;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Medico
{
    public class GetAllMedicoHandler : IRequestHandler<GetAllMedicoQuery, IEnumerable<MedicoEntity>>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetAllMedicoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<IEnumerable<MedicoEntity>> Handle(GetAllMedicoQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<MedicoEntity>("Medicos");

            var medicos = await collection.Find(Builders<MedicoEntity>.Filter.Empty)
                                .Project<MedicoEntity>(
                                    Builders<MedicoEntity>.Projection
                                    .Exclude("_id"))
                                .ToListAsync(cancellationToken);

            return medicos;
        }
    }
}
