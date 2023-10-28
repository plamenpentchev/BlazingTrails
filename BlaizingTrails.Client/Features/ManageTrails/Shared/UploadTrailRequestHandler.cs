using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;

namespace BlazingTrails.Client.Features.ManageTrails.Shared
{
    public class UploadTrailRequestHandler :
        IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public UploadTrailRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
        {
            //... get file content
            var fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken);
            if (fileContent == null)
            {
                throw new ArgumentException("Failed to open stream to file.");
            }
            //... build web cntent
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(fileContent), "image", request.File.Name);

            //...send request
            var response = await _httpClient.PostAsync(
                UploadTrailImageRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString()),
                content, cancellationToken);

            //...interpret response
            if (response.IsSuccessStatusCode)
            {
                var fileName = await response.Content.ReadAsStringAsync(cancellationToken);
                return new UploadTrailImageRequest.Response(fileName);
            }
            else
            {
                return new UploadTrailImageRequest.Response(string.Empty);
            }
        }
    }
}
