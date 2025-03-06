using System;
using FluentValidation;
using MediatR;

namespace Application.Core;

public class ValidationBehavior<Trequest, Tresponse>(IValidator<Trequest>? validator = null)
: IPipelineBehavior<Trequest, Tresponse> where Trequest : notnull
{
    public async Task<Tresponse> Handle(Trequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
    {
        if(validator == null) return await next();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid){
            throw new ValidationException(validationResult.Errors);
        }

        return await next();
    }
}
