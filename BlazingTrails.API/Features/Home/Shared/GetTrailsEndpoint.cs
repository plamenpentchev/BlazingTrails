using Ardalis.ApiEndpoints;
using BlazingTrails.API.Persistence;
using BlazingTrails.Shared.Features.Home.Shared;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
                .Include(x => x.Waypoints)
                .ToListAsync(cancellationToken);
          
            return Ok(new GetTrailsRequest.Response(
                trails.Select(trail => new GetTrailsRequest.Trail(
                    trail.Id, 
                    trail.Name, 
                    trail.Location, 
                    trail.Image, 
                    trail.TimeInMinutes, 
                    trail.Length, 
                    trail.Description, 
                    trail.Waypoints.Select(wp => new GetTrailsRequest.Waypoint(wp.Latitude, wp.Longitude)).ToList()
                ))));
        }
    }
}
