using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PCIPalApp.Models
{
    [KnownType(typeof(SessionRequestPara))]
    public class SessionRequestPara
    {
        public int FlowId { get; set; }
        public IntialValuesPara InitialValues { get; set; }

    }
    public class IntialValuesPara
    {
        public string key { get; set; }
    }

    [DataContract]
    public class SessionResponse
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string LinkId { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }


    }
}