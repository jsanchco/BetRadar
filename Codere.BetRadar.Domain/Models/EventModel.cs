using System;
using System.Collections.Generic;
using System.Text;

namespace Codere.BetRadar.Domain.Models
{
    public class EventModel
    {

        public string id { get; set; }
        public string name { get; set; }
        public List<StreamModel> streams { get; set; }

    }
}
