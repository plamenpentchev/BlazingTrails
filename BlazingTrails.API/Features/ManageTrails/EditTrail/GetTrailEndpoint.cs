using Ardalis.ApiEndpoints;
using BlazingTrails.API.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.API.Features.ManageTrails.EditTrail
{
    public class GetTrailEndpoint : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<GetTrailRequest.Response>
    {
        private readonly BlazingTrailsContext _database;

        public GetTrailEndpoint(BlazingTrailsContext database)
        {
            _database = database;
        }
        [HttpGet(GetTrailRequest.RouteTemplate)]
        public async override Task<ActionResult<GetTrailRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
        {
            var trail = await _database.Trails
                .Include(x => x.Waypoints)
                .SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken: cancellationToken);
            if (trail is null)
            {
                return BadRequest("Trail could not be find.");
            }

            var response = new GetTrailRequest.Response(
                new GetTrailRequest.Trail(
                    trail.Id,
                    trail.Name,
                    trail.Location,
                    trail.Image,
                    trail.TimeInMinutes,
                    trail.Length,
                    trail.Description,
                    trail.Waypoints.Select( wp => new GetTrailRequest.Waypoint(wp.Latitude, wp.Longitude)))
                );
            return Ok(response);
        }
    }
}
