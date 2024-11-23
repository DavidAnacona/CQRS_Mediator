using CQRS_Distribuidos.Infrastructure.Queries.Consulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Consulta
{
    public class GetByIdConsultaHandler : IRequestHandler<GetByIdConsultaQuery, ConsultaEntity>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetByIdConsultaHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<ConsultaEntity> Handle(GetByIdConsultaQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<ConsultaEntity>("Consultas");

            var filter = Builders<ConsultaEntity>.Filter.Eq(c => c.IdConsulta, request.Id);

            var projection = Builders<ConsultaEntity>.Projection.Exclude("_id");

            var consulta = await collection.Find(filter)
                                         .Project<ConsultaEntity>(projection)
                                         .FirstOrDefaultAsync(cancellationToken);

            return consulta;
        }
    }
}
