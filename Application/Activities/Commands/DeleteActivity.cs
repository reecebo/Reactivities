using System;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class DeleteActivity 
{
    public class Command : IRequest
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            //Gets the Activity with a specified Id from the DB --returns exception if Activity is not found
            var activity = await context.Activities
                .FindAsync([request.Id], cancellationToken ) 
                    ?? throw new Exception("Cannot find activity");
            
            //Removes the Activity from the DB
            context.Remove(activity);
            
            //Commits the changes to the DB
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
