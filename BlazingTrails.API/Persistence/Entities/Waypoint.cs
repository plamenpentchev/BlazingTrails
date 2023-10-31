﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.API.Persistence.Entities
{
    public class Waypoint
    {
        public int Id { get; set; }
        public int TrailId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public Trail Trail { get; set; } = default!;
    }

    public class WaypointConfigs : IEntityTypeConfiguration<Waypoint>
    {
        public void Configure(EntityTypeBuilder<Waypoint> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Longitude).IsRequired();
        }
    }
}
