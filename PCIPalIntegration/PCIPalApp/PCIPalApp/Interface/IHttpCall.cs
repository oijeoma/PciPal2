using PCIPalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace PCIPalApp.Interface
{
    internal interface IHttpCall
    {
        Task<Response> GetToken(KeyValuePair<string, string>[] RequestPara);
        Task<SessionResponse> GetPciPalSession(SessionRequestPara RequestPara, string access_token);

        Task<JsonResult> GetPciPalDisplaySession(string iframeUrl, string access_token, string refresh_token);
    }
}
