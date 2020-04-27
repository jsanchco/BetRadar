namespace Codere.BetRadar.ServiceEventsBetRadar
{ 
    #region Using

    using Codere.BetRadar.Domain.Services;
    using Codere.BetRadar.Domain.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public class ServiceEventsBetRadar : IServiceEvents
    {
        public Event GetEvent(int eventId)
        {
            // Mock
            return (new Event
            {
                Id = eventId
            });
        }

        public ListEvents GetEvents()
        {
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
    }
}
