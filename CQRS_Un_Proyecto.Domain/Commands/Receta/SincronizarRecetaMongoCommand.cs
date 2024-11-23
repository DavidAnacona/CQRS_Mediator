using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Receta
{
    public class SincronizarRecetaMongoCommand : IRequest<Unit>
    {
        public RecetaEntity Receta { get; set; }

        public SincronizarRecetaMongoCommand(RecetaEntity receta)
        {
            Receta = receta;
        }
    }
}
