using System;
using System.Diagnostics;
using MediatR;
using Domain;
using Persistence;
using System.Runtime.CompilerServices;

namespace Application.Activities.Commands;

public class CreateActivity
{
    public class Command : IRequest<string>
    {
        public required Domain.Activity Activity { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command,string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            context.Activities.Add(request.Activity);

            await context.SaveChangesAsync(cancellationToken);

            return request.Activity.Id;
        }

    }
}
