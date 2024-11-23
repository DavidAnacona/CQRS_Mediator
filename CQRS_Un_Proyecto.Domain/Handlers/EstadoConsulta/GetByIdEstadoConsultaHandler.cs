using CQRS_Distribuidos.Infrastructure.Queries.EstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.EstadoConsulta
{
    public class GetByIdEstadoConsultaHandler : IRequestHandler<GetByIdEstadoConsultaQuery, EstadoConsultaEntity>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetByIdEstadoConsultaHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<EstadoConsultaEntity> Handle(GetByIdEstadoConsultaQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<EstadoConsultaEntity>("EstadoConsulta");

            var filter = Builders<EstadoConsultaEntity>.Filter.Eq(e => e.IdEstadoConsulta, request.Id);

            var projection = Builders<EstadoConsultaEntity>.Projection.Exclude("_id");

            var estado = await collection.Find(filter)
                              .Project<EstadoConsultaEntity>(projection)
                              .FirstOrDefaultAsync(cancellationToken);

            return estado;
        }
    }
}
