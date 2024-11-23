using CQRS_Distribuidos.Infrastructure.Commands.Medico;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Medico
{
    public class EliminarMedicoMongoHandler : IRequestHandler<EliminarMedicoMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public EliminarMedicoMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(EliminarMedicoMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<MedicoEntity>("Medicos");
            var filter = Builders<MedicoEntity>.Filter.Eq(m => m.IdMedico, request.Id);

            await collection.DeleteOneAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
