using CQRS_Distribuidos.Infrastructure.Commands.Paciente;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Paciente
{
    public class SincronizarPacienteMongoHandler : IRequestHandler<SincronizarPacienteMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public SincronizarPacienteMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(SincronizarPacienteMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<PacienteEntity>("Pacientes");

            // Define el filtro y las actualizaciones para MongoDB
            var filter = Builders<PacienteEntity>.Filter.Eq(p => p.IdPaciente, request.Paciente.IdPaciente);
            var update = Builders<PacienteEntity>.Update
                .Set(p => p.Nombre, request.Paciente.Nombre)
                .Set(p => p.Apellido, request.Paciente.Apellido)    
                .Set(p => p.FechaNacimiento, request.Paciente.FechaNacimiento)
                .Set(p => p.Direccion, request.Paciente.Direccion)
                .Set(p => p.Telefono, request.Paciente.Telefono)
                .Set(p => p.Correo, request.Paciente.Correo);

            // Ejecuta la operación de actualización en MongoDB
            await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);

            return Unit.Value;
        }
    }
}
