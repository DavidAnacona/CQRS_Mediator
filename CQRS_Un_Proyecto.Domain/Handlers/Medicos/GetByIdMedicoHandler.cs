using CQRS_Distribuidos.Infrastructure.Queries.Medico;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Medico
{
    public class GetByIdMedicoHandler : IRequestHandler<GetByIdMedicoQuery, MedicoEntity>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetByIdMedicoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<MedicoEntity> Handle(GetByIdMedicoQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<MedicoEntity>("Medicos");

            var filter = Builders<MedicoEntity>.Filter.Eq(m => m.IdMedico, request.Id);

            var projection = Builders<MedicoEntity>.Projection.Exclude("_id");

            var medico = await collection.Find(filter)
                                         .Project<MedicoEntity>(projection)
                                         .FirstOrDefaultAsync(cancellationToken);

            return medico;
        }
    }
}
