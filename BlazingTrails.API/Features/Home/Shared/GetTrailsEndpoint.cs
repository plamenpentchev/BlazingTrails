using Ardalis.ApiEndpoints;
using BlazingTrails.API.Persistence;
using BlazingTrails.Shared.Features.Home.Shared;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.API.Features.Home.Shared
{
    public class GetTrailsEndpoint : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<GetTrailsRequest.Response>
    {
        private readonly BlazingTrailsContext _context;

        public GetTrailsEndpoint(BlazingTrailsContext context)
        {
            _context = context;
        }
        [HttpGet(GetTrailsRequest.RouteTemplate)]
        public override async Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(int request, CancellationToken cancellationToken = default)
        {
            var trails = await _context.Trails
                .Include(x => x.Route).ToListAsync();

            return Ok(new GetTrailsRequest.Response(
                trails.Select(x => new GetTrailsRequest.Trail(
                    x.Id, x.Name, x.Location, x.Image, x.TimeInMinutes, x.Length, x.Description
                ))));
        }
    }
}
