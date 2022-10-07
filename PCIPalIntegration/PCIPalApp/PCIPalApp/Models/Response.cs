using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PCIPalApp.Models
{
    [DataContract]
    
    public class Response
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public int expires_in { get; set; }
        [DataMember]
        public string refresh_token { get; set; }
        [DataMember]
        public string client_id { get; set; }
        [DataMember]
        public string tenantName { get; set; }

    }
}