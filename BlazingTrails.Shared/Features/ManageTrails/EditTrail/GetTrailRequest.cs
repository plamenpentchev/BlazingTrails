﻿using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlazingTrails.Shared.Features.ManageTrails.Shared.TrailDto;

namespace BlazingTrails.Shared.Features.ManageTrails.EditTrail
{
   public record GetTrailRequest(int TrailId):IRequest<GetTrailRequest.Response>
    {
        public const string RouteTemplate = "/trails/{trailId}";

        public record Response(Trail Trail);

        public record Trail(int Id, 
            string Name, 
            string Location, 
            string? Image, 
            int TimeInMinutes,
            int Length,
            string Description,
            IEnumerable<RouteInstruction> RouteInstructions);

        public record RouteInstruction (int Id, int Stage, string Description);

    }
}
