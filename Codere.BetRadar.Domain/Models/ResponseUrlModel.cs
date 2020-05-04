using System;
using System.Collections.Generic;
using System.Text;

namespace Codere.BetRadar.Domain.Models
{
    public class ResponseUrlModel
    {
        public DateTime HoraSolicitud { get; set; }
        public List<EventModel> EventList { get; set; }
    }
}
