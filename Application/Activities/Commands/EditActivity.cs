using System;
using MediatR;
using Domain;
using Persistence;
using AutoMapper;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest
    {
        public required Domain.Activity Activity { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities
                .FindAsync([request.Activity.Id], cancellationToken ) 
                    ?? throw new Exception("Cannot find activity");

            mapper.Map(request.Activity, activity);

            await context.SaveChangesAsync(cancellationToken);

        }
    }


}
