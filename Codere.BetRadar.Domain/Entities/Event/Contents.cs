namespace Codere.BetRadar.Domain.Entities
{
    #region Using

    using System;
    using System.Collections.Generic;

    #endregion

    public class Contents
    {
        public string id { get; set; }
        public string name { get; set; }
        public Content_type content_type { get; set; }
        public bool is_main { get; set; }
        public List<Streams> streams { get; set; }
    }
}
