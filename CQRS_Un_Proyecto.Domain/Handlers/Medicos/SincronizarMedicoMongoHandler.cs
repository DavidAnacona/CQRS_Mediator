using CQRS_Distribuidos.Infrastructure.Commands.Medico;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Medico
{
    public class SincronizarMedicoMongoHandler : IRequestHandler<SincronizarMedicoMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public SincronizarMedicoMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(SincronizarMedicoMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<MedicoEntity>("Medicos");

            // Define el filtro y las actualizaciones para MongoDB
            var filter = Builders<MedicoEntity>.Filter.Eq(m => m.IdMedico, request.Medico.IdMedico);
            var update = Builders<MedicoEntity>.Update
                .Set(m => m.Nombre, request.Medico.Nombre)
                .Set(m => m.Apellido, request.Medico.Apellido)
                .Set(m => m.Especialidad, request.Medico.Especialidad)
                .Set(m => m.Telefono, request.Medico.Telefono)
                .Set(m => m.Correo, request.Medico.Correo);

            // Ejecuta la operación de actualización en MongoDB
            await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);

            return Unit.Value;
        }
    }
}
