using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.Receta
{
    public class EliminarRecetaMongoCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public EliminarRecetaMongoCommand(int id)
        {
            Id = id;
        }
    }
}
