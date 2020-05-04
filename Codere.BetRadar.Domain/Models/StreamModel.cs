using System;
using System.Collections.Generic;
using System.Text;

namespace Codere.BetRadar.Domain.Models
{
    public class StreamModel
    {
        public string id { get; set; }
        //public string name { get; set; }
        public List<KeyValuePair<string,string>> UrlList { get; set; }
    }
}
