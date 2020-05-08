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
    using Codere.BetRadar.Domain.Models;
    using Codere.BetRadar.Domain.Helpers;
    using System.Linq;
    using static Codere.BetRadar.ServiceEventsBetRadar.Helpers.Enum;

    #endregion

    public class ServiceEventsBetRadar : IServiceEvents
    {
        public async Task<Event> GetEvent(int eventId)
        {
            try
            {
                var httpClient = GetClientHttp();
                string parameters = $"/av:event: {eventId}";
                string url = string.Concat(Constants.ApiEvents, parameters);
                var httpResponseMessage = await httpClient.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    Event model = JsonSerializer.Deserialize<Event>(content);

                    return model;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(httpResponseMessage.ToString());
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ListEvents> GetEvents()
        {
            try
            {
                var httpClient = GetClientHttp();
                var httpResponseMessage = await httpClient.GetAsync(Constants.ApiEvents);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    List<Event> eventsList = JsonSerializer.Deserialize<List<Event>>(content);

                    return new ListEvents
                    {
                        StartFetch = DateTime.Now,
                        events = eventsList
                    };
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(httpResponseMessage.ToString());
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ListEvents> GetEventsByStreamStatus(Nullable<int> IdStreamStatus)
        {
            try
            {
                var httpClient = GetClientHttp();
                var httpResponseMessage = await httpClient.GetAsync(Constants.ApiEvents);

                IdStreamStatus = IdStreamStatus.HasValue ? IdStreamStatus : (int)StreamStatusEnum.OnAirBroadcast;
                string StreamStatus = $"av:stream_status:{IdStreamStatus}";

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    List<Event> eventsList = JsonSerializer.Deserialize<List<Event>>(content);
                    eventsList = eventsList.Where(c => c.contents.Any(d => d.streams.Any(e => e.stream_status.id == StreamStatus))).ToList();

                    return new ListEvents
                    {
                        StartFetch = DateTime.Now,
                        events = eventsList
                    };
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(httpResponseMessage.ToString());
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<StatusModel>> GetStatuses()
        {
            try
            {
                var httpClient = GetClientHttp();
                var httpResponseMessage = await httpClient.GetAsync(Constants.ApiStatuses);


                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    IList<Status> statusList = JsonSerializer.Deserialize<List<Status>>(content);
                    IList<StatusModel> modelList = Converter.ConvertStatusEntityToModelList(statusList);
                    return modelList;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(httpResponseMessage.ToString());
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Stream> GetStream(string stream_id, string stream_type)
        {
            try
            {
                var httpClient = GetClientHttp();

                string parameters = $"/av:stream: {stream_id}/{stream_type}";
                string url = string.Concat(Constants.ApiStreams, parameters);

                var httpResponseMessage = await httpClient.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    Stream stream = JsonSerializer.Deserialize<Stream>(content);
                    //IList<StatusModel> modelList = Converter.ConvertStatusEntityToModelList(statusList);
                    return stream;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(httpResponseMessage.ToString());
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseUrlModel> GetAllStream()
        {
            try
            {
                ResponseUrlModel eventToResponse = new ResponseUrlModel();
                eventToResponse.EventList = new List<EventModel>();
                eventToResponse.HoraSolicitud = System.DateTime.Now;

                var httpClientEvents = GetClientHttp();
                var httpResponseMessageEvents = await httpClientEvents.GetAsync(Constants.ApiEvents);

                int IdStreamStatus = (int)StreamStatusEnum.OnAirBroadcast;
                string StreamStatus = $"av:stream_status:{IdStreamStatus}";

                if (httpResponseMessageEvents.IsSuccessStatusCode)
                {
                    var content = await httpResponseMessageEvents.Content.ReadAsStringAsync();
                    List<Event> eventsList = JsonSerializer.Deserialize<List<Event>>(content);
                    eventsList = eventsList.Where(c => c.contents.Any(d => d.streams.Any(e => e.stream_status.id == StreamStatus))).ToList();

                    foreach (Event events in eventsList)
                    {
                        EventModel eventmodel = Converter.ConvertEventEntityToModel(events);
                        eventmodel.streams = new List<StreamModel>();

                        List<Streams> listaStreams = new List<Streams>();
                        listaStreams = events.contents.SelectMany(a => a.streams).ToList();

                        foreach (Streams streams in listaStreams)
                        {
                            StreamModel streamModel = new StreamModel();
                            streamModel.UrlList = new List<KeyValuePair<string, string>>();
                            streamModel.id = streams.id;

                            foreach (string typeStream in Constants.TypesStreams)
                            {                                
                                var httpClientStream = GetClientHttp();
                                string parameters = $"/{streams.id}/{typeStream}";
                                string url = string.Concat(Constants.ApiStreams, parameters);

                                var httpResponseMessageStream = await httpClientStream.GetAsync(url);

                                if (httpResponseMessageStream.IsSuccessStatusCode)
                                {
                                    var contentStream = await httpResponseMessageStream.Content.ReadAsStringAsync();
                                    Stream stream = JsonSerializer.Deserialize<Stream>(contentStream);

                                    string urlComplete = typeStream == "rtmp" ?  string.Concat(stream.url, stream.stream_name) : stream.url;
                                    streamModel.UrlList.Add(new KeyValuePair<string,string>( typeStream, urlComplete));
                                }
                            }                            
                            eventmodel.streams.Add(streamModel);
                        }
                        eventToResponse.EventList.Add(eventmodel);
                    }             
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(httpResponseMessageEvents.ToString());
                    return null;
                }
                return eventToResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseStreamingInfo> GetStreamingInfo(string idEvent, bool isMovil)
        {
            try
            {
                ResponseStreamingInfo response = new ResponseStreamingInfo();      

                var httpClient = GetClientHttp();
                string parameters = $"/av:event: {idEvent}";
                string url = string.Concat(Constants.ApiEvents, parameters);
                var httpResponseMessage = await httpClient.GetAsync(url);

                int IdStreamStatus = (int)StreamStatusEnum.OnAirBroadcast;
                string StreamStatus = $"av:stream_status:{IdStreamStatus}";

                bool isLive = false;

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    Event model = JsonSerializer.Deserialize<Event>(content);

                    isLive = model.contents.Any(d => d.streams.Any(e => e.stream_status.id == StreamStatus));

                    List<Streams> listaStreams = new List<Streams>();
                    listaStreams = model.contents.SelectMany(a => a.streams).ToList();

                    foreach (Streams streams in listaStreams)
                    {
                        StreamModel streamModel = new StreamModel();
                        streamModel.UrlList = new List<KeyValuePair<string, string>>();
                        streamModel.id = streams.id;

                        foreach (string typeStream in Constants.TypesStreams)
                        {
                            var httpClientStream = GetClientHttp();
                            string parametersStream = $"/{streams.id}/{typeStream}";
                            string urlStream = string.Concat(Constants.ApiStreams, parametersStream);

                            var httpResponseMessageStream = await httpClientStream.GetAsync(urlStream);

                            if (httpResponseMessageStream.IsSuccessStatusCode)
                            {
                                var contentStream = await httpResponseMessageStream.Content.ReadAsStringAsync();
                                Stream stream = JsonSerializer.Deserialize<Stream>(contentStream);

                                string urlComplete = typeStream == "rtmp" ? string.Concat(stream.url, stream.stream_name) : stream.url;

                                response.URL = urlComplete;
                                response.IsLive = isLive;

                                return response;
                            }
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(httpResponseMessage.ToString());
                }
                return null;                          
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Metodos privados 

        private HttpClient GetClientHttp()
        {
            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(Constants.Uri);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.Bearer);
            httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.Add("X-Real-IP", "52.423.44.33");

            return httpClient;
        }

        #endregion
    }
}
