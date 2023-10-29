using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingTrails.Shared.Features.Home.Shared
{
   public record GetTrailsRequest():IRequest<GetTrailsRequest.Response>
    {
        public const string RouteTemplate = "/trails";

        public record Response(IEnumerable<Trail> Trails);

        public record Trail(int Id,
            string Name,
            string Location,
            string? Image,
            int TimeInMinutes,
            int Length,
            string Description);

    }
}
