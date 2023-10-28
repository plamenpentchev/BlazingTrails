using Ardalis.ApiEndpoints;
using BlazingTrails.API.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BlazingTrails.API.Features.ManageTrails.Shared
{
    public class UploadTrailImageEndpoint : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<string>
    {
        private readonly BlazingTrailsContext _database = default!;

        public UploadTrailImageEndpoint(BlazingTrailsContext context)
        {
            _database = context;
        }

        [HttpPost(UploadTrailImageRequest.RouteTemplate)]
        public override async Task<ActionResult<string>> HandleAsync(
            [FromRoute] int trailId,
            CancellationToken cancellationToken = default)
        {
            var trail = await _database.Trails.SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken);
            if (trail == null)
            {
                return BadRequest("The trail does not exist.");
            }
            var file = Request.Form.Files[0];
            if (file.Length == 0)
            {
                return BadRequest("No image found");
            }

            var fileName = $"{Guid.NewGuid()}.jpg";
            var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), @"Images", fileName);
            var resizeOptions = new ResizeOptions()
            {
                Mode = ResizeMode.Pad,
                Size = new Size(640, 426)
            };
            using var image = Image.Load(file.OpenReadStream());
            image.Mutate(x => x.Resize(resizeOptions));
            await image.SaveAsJpegAsync(saveLocation, cancellationToken: cancellationToken);

            if (!string.IsNullOrEmpty(trail.Image))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image));   
            }

            trail.Image = fileName;

            await _database.SaveChangesAsync(cancellationToken: cancellationToken);
            return Ok(trail.Image);
        }
    }
}
