namespace Codere.BetRadar.ServiceEventsBetRadar
{ 
    #region Using

    using Codere.BetRadar.Domain.Services;
    using Codere.BetRadar.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Codere.BetRadar.ServiceEventsBetRadar.Helpers;
    using System.Text.Json;

    #endregion

    public class ServiceEventsBetRadar : IServiceEvents
    {
        public async Task<Event> GetEvent(int eventId)
        {
            // Mock
            return (new Event
            {
                Id = eventId
            });
        }

        public async Task<ListEvents> GetEvents()
        {
            var httpClient = GetClientHttp();
            var httpResponseMessage = await httpClient.GetAsync(Constants.ApiEvents);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(content);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(httpResponseMessage.ToString());
            }

            // Mock
            var listEvents = new List<Event>
            {
                new Event { Id = 1 },
                new Event { Id = 2 }
            };

            return new ListEvents
            {
                StartFetch = DateTime.Now,
                Events = listEvents
            };
        }

        private HttpClient GetClientHttp()
        {
            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(Constants.Uri);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.Bearer);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("X-Real-IP", "52.423.44.33");
            
            return httpClient;
        }
    }
}
