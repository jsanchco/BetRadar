namespace Codere.BetRadar.Domain.Entities
{
    #region Using

    using System;
    using System.Collections.Generic;

    #endregion

    public class ListEvents
    {
        public DateTime StartFetch { get; set; }
        public List<Event> events { get; set; }
    }
}
