using CQRS_Distribuidos.Infrastructure.Queries.Paciente;
using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Distribuidos.Application.Handlers.Paciente
{
    public class GetAllPacienteHandler : IRequestHandler<GetAllPacienteQuery, IEnumerable<PacienteEntity>>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetAllPacienteHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<IEnumerable<PacienteEntity>> Handle(GetAllPacienteQuery request, CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<PacienteEntity>("Pacientes");

            var pacientes = await collection.Find(Builders<PacienteEntity>.Filter.Empty)
                                .Project<PacienteEntity>(
                                    Builders<PacienteEntity>.Projection
                                    .Exclude("_id"))
                                .ToListAsync(cancellationToken);

            return pacientes;
        }
    }
}
