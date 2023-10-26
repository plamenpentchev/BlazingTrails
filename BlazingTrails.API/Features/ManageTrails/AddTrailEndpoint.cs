﻿using Ardalis.ApiEndpoints;
using BlazingTrails.API.Persistence;
using BlazingTrails.API.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.API.Features.ManageTrails
{
    public class AddTrailEndpoint : BaseAsyncEndpoint
        .WithRequest<AddTrailRequest>
        .WithResponse<int>
    {
        private readonly BlazingTrailsContext _database;

        public AddTrailEndpoint(BlazingTrailsContext database)
        {
            _database = database;
        }

        [HttpPost(AddTrailRequest.RouteTemplate)]
        public override async Task<ActionResult<int>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var trail = new Trail
                {
                    Name = request.Trail.Name,
                    Description = request.Trail.Description,
                    Location = request.Trail.Location,
                    TimeInMinutes = request.Trail.TimeInMinutes,
                    Length = request.Trail.Length
                };

                await _database.Trails.AddAsync(trail, cancellationToken);

                var routeInstructions = request.Trail.Route.Select(x => new RouteInstruction
                {
                    Stage = x.Stage,
                    Description = x.Description,
                    Trail = trail
                });

                await _database.RouteInstructions.AddRangeAsync(routeInstructions.ToList(), cancellationToken);
                await _database.SaveChangesAsync(cancellationToken);

                return Ok(trail.Id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
           

        }
    }
}
