using DTOs.Command;
using DTOs.Response;
using MediatR;
namespace InvApi.Handler
{
    public class ItemCommandHandler : IRequestHandler<CreateItemCommand, CreateItemCommandResponse>
    {
        public ItemCommandHandler()//(AppDbContext dbContext)
        {
            
        }

        public async Task<CreateItemCommandResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {

            CreateItemCommandResponse obj = new CreateItemCommandResponse();
            return obj; // Returning the ID of the created order
        }
    }
}
