using System;
using System.Diagnostics;
using MediatR;
using Domain;
using Persistence;
using System.Runtime.CompilerServices;
using Application.Activities.DTOs;
using AutoMapper;
using FluentValidation;
using Application.Core;
using Application.Interfaces;

namespace Application.Activities.Commands;

public class CreateActivity
{
    public class Command : IRequest<Result<string>>
    {
        public required CreateActivityDto ActivityDto { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper, IUserAccessor userAccessor) 
        : IRequestHandler<Command,Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await userAccessor.GetUserAsync();

            var activity = mapper.Map<Domain.Activity>(request.ActivityDto);

            context.Activities.Add(activity);

            var attendee = new ActivityAttendee {
                ActivityId = activity.Id,
                UserId = user.Id,
                isHost = true
            };

            activity.Attendees.Add(attendee);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if(!result) return Result<string>.Failure("Failure to create activity", 400);

            return Result<string>.Success(activity.Id);
        }

    }
}
