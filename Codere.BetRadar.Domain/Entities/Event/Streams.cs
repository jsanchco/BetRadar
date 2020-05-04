namespace Codere.BetRadar.Domain.Entities
{
    #region Using

    using System;
    using System.Collections.Generic;

    #endregion

    public class Streams
    {
        public string id { get; set; }

        public Product product { get; set; }
        public Distribution distribution { get; set; }

        public string start_time { get; set; }
        public string end_time { get; set; }

        public Stream_status stream_status { get; set; }

        public List<Languages> languages { get; set; }
        public List<Geo_restrictions> geo_restrictions { get; set; }
        
    }
}
