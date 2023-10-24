using BlazingTrails.Shared.Features.ManageTrails;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.ManageTrails
{
    public class AddTrailRequestHandler : IRequestHandler<AddTrailRequest, AddTrailRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public AddTrailRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AddTrailRequest.Response> Handle(AddTrailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<AddTrailRequest>(AddTrailRequest.RouteTemplate, request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var trailId = await response.Content.ReadFromJsonAsync<int>();
                        return new AddTrailRequest.Response(trailId);
                    }
                    return new AddTrailRequest.Response(-1);

                }
                else
                {
                    return new AddTrailRequest.Response(-1);
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
