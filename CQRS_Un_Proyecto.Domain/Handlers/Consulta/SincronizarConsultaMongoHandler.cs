using CQRS_Distribuidos.Infrastructure.Commands.Consulta;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Consulta
{
    public class SincronizarConsultaMongoHandler : IRequestHandler<SincronizarConsultaMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public SincronizarConsultaMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(SincronizarConsultaMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<ConsultaEntity>("Consultas");

            // Define el filtro y las actualizaciones para MongoDB
            var filter = Builders<ConsultaEntity>.Filter.Eq(c => c.IdConsulta, request.Consulta.IdConsulta);
            var update = Builders<ConsultaEntity>.Update
                .Set(c => c.IdPaciente, request.Consulta.IdPaciente)
                .Set(c => c.IdMedico, request.Consulta.IdMedico)
                .Set(c => c.Notas, request.Consulta.Notas)
                .Set(c => c.IdEstadoConsulta, request.Consulta.IdEstadoConsulta);

            // Ejecuta la operación de actualización en MongoDB
            await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);

            return Unit.Value;
        }
    }
}
