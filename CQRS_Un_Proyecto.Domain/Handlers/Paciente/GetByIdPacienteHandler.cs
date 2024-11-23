using CQRS_Distribuidos.Infrastructure.Queries.Paciente;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Paciente
{
    public class GetByIdPacienteHandler : IRequestHandler<GetByIdPacienteQuery, PacienteEntity>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetByIdPacienteHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<PacienteEntity> Handle(GetByIdPacienteQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<PacienteEntity>("Pacientes");

            var filter = Builders<PacienteEntity>.Filter.Eq(p => p.IdPaciente, request.Id);

            var projection = Builders<PacienteEntity>.Projection.Exclude("_id");

            var paciente = await collection.Find(filter)
                                           .Project<PacienteEntity>(projection) 
                                           .FirstOrDefaultAsync(cancellationToken);

            return paciente;
        }
    }
}
