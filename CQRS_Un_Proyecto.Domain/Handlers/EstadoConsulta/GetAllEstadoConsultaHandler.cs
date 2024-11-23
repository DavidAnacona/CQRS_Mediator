using CQRS_Distribuidos.Infrastructure.Queries.EstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.EstadoConsulta
{
    public class GetAllEstadoConsultaHandler : IRequestHandler<GetAllEstadoConsultaQuery, IEnumerable<EstadoConsultaEntity>>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetAllEstadoConsultaHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<IEnumerable<EstadoConsultaEntity>> Handle(GetAllEstadoConsultaQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<EstadoConsultaEntity>("EstadoConsulta");

            var estados = await collection.Find(Builders<EstadoConsultaEntity>.Filter.Empty)
                              .Project<EstadoConsultaEntity>(
                                  Builders<EstadoConsultaEntity>.Projection.Exclude("_id"))
                              .ToListAsync(cancellationToken);

            return estados;
        }
    }
}
