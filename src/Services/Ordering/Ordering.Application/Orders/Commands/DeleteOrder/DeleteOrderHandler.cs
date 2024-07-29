
namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IApplicationDbContext context) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == OrderId.Of(command.Id));

            if(order is null)
            {
                throw new OrderNotFoundException(command.Id);
            }

            context.Orders.Remove(order);

            await context.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
