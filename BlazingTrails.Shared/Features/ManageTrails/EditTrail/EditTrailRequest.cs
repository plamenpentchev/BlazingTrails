﻿using BlazingTrails.Shared.Features.ManageTrails.Shared;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingTrails.Shared.Features.ManageTrails.EditTrail
{
    public record EditTrailRequest(TrailDto Trail) : IRequest<EditTrailRequest.Response>
    {
        public const string RouteTemplate = "/trails";
        public record Response(bool IsSuccess);
       
    }

    public class EditRequestValidator: AbstractValidator<EditTrailRequest>
    {
        public EditRequestValidator()
        {
            RuleFor(x => x.Trail).SetValidator(new TrailDtoValidator());
        }
    }
}
