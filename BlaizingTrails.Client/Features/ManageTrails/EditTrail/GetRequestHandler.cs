using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.ManageTrails.EditTrail
{
    public class GetRequestHandler : IRequestHandler<GetTrailRequest, GetTrailRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public GetRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetTrailRequest.Response?> Handle(GetTrailRequest request, CancellationToken cancellationToken)
        {
            return await _httpClient
                 .GetFromJsonAsync<GetTrailRequest.Response>
                 (GetTrailRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString()), cancellationToken);
               
        }
    }
}
