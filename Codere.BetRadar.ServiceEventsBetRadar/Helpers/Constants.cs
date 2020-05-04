using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Codere.BetRadar.ServiceEventsBetRadar.Helpers
{
    public static class Constants
    {
        public const string Uri = "https://av-api.betradar.com/";
        public const string ApiStatuses = "v1/event-statuses";
        public const string ApiEvents = "v1/events";
        public const string ApiStreams = "v1/streams";
        public const string Bearer = "ojWXwjckXQC5iazOvC";

        public static readonly IList<string> TypesStreams = new ReadOnlyCollection<string>
                                                           (new List<String> {
                                                               "rtmp",
                                                               "dash-manifest",
                                                               "cmaf-manifest",
                                                               "hls",
                                                               "hls-manifest"});
    }
}
