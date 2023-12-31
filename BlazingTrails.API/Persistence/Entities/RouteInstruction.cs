﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.API.Persistence.Entities
{
    public class RouteInstruction
    {
        public int Id { get; set; }
        public int TrailId { get; set; }
        public int Stage { get; set; }
        public string Description { get; set; } = string.Empty;

        public Trail Trail { get; set; } = default!;
    }

    public class RouteInstructionConfig : IEntityTypeConfiguration<RouteInstruction>
    {
        public void Configure(EntityTypeBuilder<RouteInstruction> builder)
        {
            builder.Property(x => x.TrailId).IsRequired();
            builder.Property( x => x.Stage).IsRequired();
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
