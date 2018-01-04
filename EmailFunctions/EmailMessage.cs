using Newtonsoft.Json;
using System.Collections.Generic;

namespace EmailFunctions
{
    public class EmailMessage
    {
        [JsonProperty(PropertyName = "fromAddress")]
        public string FromEmailAddress { get; set; }

        [JsonProperty(PropertyName = "fromName")]
        public string FromName { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "isHtml")]
        public bool IsHtml { get; set; }

        [JsonProperty(PropertyName = "toAddresses") ]
        public List<string> ToEmailAddresses { get; } = new List<string>();
    }
}
