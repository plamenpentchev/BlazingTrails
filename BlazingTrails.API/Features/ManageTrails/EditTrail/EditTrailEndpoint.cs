﻿using Ardalis.ApiEndpoints;
using BlazingTrails.API.Persistence;
using BlazingTrails.API.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.API.Features.ManageTrails.EditTrail
{
    public class EditTrailEndpoint : BaseAsyncEndpoint
        .WithRequest<EditTrailRequest>
        .WithResponse<bool>
    {
        private readonly BlazingTrailsContext _database;

        public EditTrailEndpoint(BlazingTrailsContext database)
        {
            _database = database;
        }
        [HttpPut(EditTrailRequest.RouteTemplate)]
        public async override Task<ActionResult<bool>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = default)
        {
            var trail = await _database.Trails
                 .Include(x => x.Waypoints)
                 .SingleOrDefaultAsync(x => x.Id == request.Trail.Id, cancellationToken: cancellationToken);
            if (trail is null)
            {
                return BadRequest("Trail could not be found.");
            }
            trail.Name = request.Trail.Name;
            trail.Description = request.Trail.Description;
            trail.Location = request.Trail.Location;
            trail.Length = request.Trail.Length;
            trail.TimeInMinutes = request.Trail.TimeInMinutes;
            trail.Waypoints = request.Trail.Waypoints.Select( wp => new Waypoint 
            {
                Latitude = wp.Latitude,
                Longitude = wp.Longitude,
            }).ToList();
            //trail.Route =request.Trail.Route.Select(x => new Persistence.Entities.RouteInstruction()
            //{
            //    Stage = x.Stage,
            //    Description = x.Description,
            //    Trail = trail
            //}).ToList();
            if (request.Trail.ImageAction ==ImageAction.Remove)
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), @"Images", trail.Image));
                trail.Image = string.Empty;
            }

            await _database.SaveChangesAsync(cancellationToken);
            return Ok(true);
        }
    }
}
