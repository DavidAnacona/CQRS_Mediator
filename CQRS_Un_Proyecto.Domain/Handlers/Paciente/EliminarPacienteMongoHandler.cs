using CQRS_Distribuidos.Infrastructure.Commands.Paciente;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Paciente
{
    public class EliminarPacienteMongoHandler : IRequestHandler<EliminarPacienteMongoCommand, Unit>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public EliminarPacienteMongoHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Unit> Handle(EliminarPacienteMongoCommand request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<PacienteEntity>("Pacientes");
            var filter = Builders<PacienteEntity>.Filter.Eq(p => p.IdPaciente, request.Id);

            await collection.DeleteOneAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
