using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.ManageTrails.EditTrail
{
    public class EditTrailRequestHandler : IRequestHandler<EditTrailRequest, EditTrailRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public EditTrailRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EditTrailRequest.Response> Handle(EditTrailRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PutAsJsonAsync(EditTrailRequest.RouteTemplate, request);
            if (response.IsSuccessStatusCode)
            {
                return new EditTrailRequest.Response(true);
            }
            return new EditTrailRequest.Response(false);
        }
    }
}
