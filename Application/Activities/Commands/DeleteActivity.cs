using System;
using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class DeleteActivity 
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            //Gets the Activity with a specified Id from the DB --returns exception if Activity is not found
            var activity = await context.Activities
                .FindAsync([request.Id], cancellationToken);

            if ( activity == null ) return Result<Unit>.Failure("Activity not found", 404);

            //Removes the Activity from the DB
            context.Remove(activity);
            
            //Commits the changes to the DB
            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if(!result) return Result<Unit>.Failure("Failure to delete activity", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
