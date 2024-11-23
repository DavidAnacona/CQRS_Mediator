using CQRS_Distribuidos.Infrastructure.Queries.Consulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Consulta
{
    public class GetAllConsultaHandler : IRequestHandler<GetAllConsultaQuery, IEnumerable<ConsultaEntity>>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetAllConsultaHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<IEnumerable<ConsultaEntity>> Handle(GetAllConsultaQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<ConsultaEntity>("Consultas");

            var consultas = await collection.Find(Builders<ConsultaEntity>.Filter.Empty)
                                 .Project<ConsultaEntity>(
                                     Builders<ConsultaEntity>.Projection.Exclude("_id"))
                                 .ToListAsync(cancellationToken);

            return consultas;
        }
    }
}
