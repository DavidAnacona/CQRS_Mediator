using CQRS_Distribuidos.Infrastructure.Commands.EstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.EstadoConsulta
{
    public class EliminarEstadoConsultaMongoHandler : IRequestHandler<EliminarEstadoConsultaMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public EliminarEstadoConsultaMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(EliminarEstadoConsultaMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<EstadoConsultaEntity>("EstadoConsulta");
            var filter = Builders<EstadoConsultaEntity>.Filter.Eq(e => e.IdEstadoConsulta, request.Id);

            await collection.DeleteOneAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
