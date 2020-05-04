namespace Codere.BetRadar.Domain.Entities
{
    #region Using

    using System;
    using System.Collections.Generic;

    #endregion

    public class Geo_restrictions
    {
        public Device_categories device_categories { get; set; }
        public string country_iso_alpha2_codes { get; set; }
        public List<KeyValuePair<string, string>> country_subdivision_iso_codes { get; set; }
    }
}
