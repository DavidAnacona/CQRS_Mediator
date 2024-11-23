using CQRS_Distribuidos.Infrastructure.Commands.Consulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Consulta
{
    public class EliminarConsultaMongoHandler : IRequestHandler<EliminarConsultaMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public EliminarConsultaMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(EliminarConsultaMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<ConsultaEntity>("Consultas");
            var filter = Builders<ConsultaEntity>.Filter.Eq(c => c.IdConsulta, request.Id);

            await collection.DeleteOneAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
