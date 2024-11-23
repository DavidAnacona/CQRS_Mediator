using CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.HistorialEstadoConsulta
{
    public class SincronizarHistorialEstadoConsultaMongoHandler : IRequestHandler<SincronizarHistorialEstadoConsultaMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public SincronizarHistorialEstadoConsultaMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(SincronizarHistorialEstadoConsultaMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<HistorialEstadoConsultaEntity>("HistorialEstadoConsulta");

            // Define el filtro y las actualizaciones para MongoDB
            var filter = Builders<HistorialEstadoConsultaEntity>.Filter.Eq(h => h.IdHistorialEstado, request.HistorialEstadoConsulta.IdHistorialEstado);
            var update = Builders<HistorialEstadoConsultaEntity>.Update
                .Set(h => h.IdConsulta, request.HistorialEstadoConsulta.IdConsulta)
                .Set(h => h.IdEstadoConsulta, request.HistorialEstadoConsulta.IdEstadoConsulta)
                .Set(h => h.FechaCambio, request.HistorialEstadoConsulta.FechaCambio)
                .Set(h => h.UsuarioResponsable, request.HistorialEstadoConsulta.UsuarioResponsable)
                .Set(h => h.Comentario, request.HistorialEstadoConsulta.Comentario);

            // Ejecuta la operación de actualización en MongoDB
            await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);

            return Unit.Value;
        }
    }
}
