using System;
using System.Collections.Generic;

namespace Codere.BetRadar.Domain.Entities
{
    public class Event
    {
        public string id { get; set; }
        public string client_event_id { get; set; }        
        public string sport_event_id { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public Event_status event_status { get; set; }
        public First_level_category first_level_category { get; set; }
        public Second_level_category second_level_category { get; set; }
        public Third_level_category third_level_category { get; set; }
        public List<Competitors> competitors { get; set; }
        public Venue venue { get; set; }
        public List<Contents> contents { get; set; }
    }
}
