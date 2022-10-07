using PCIPalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PCIPalApp.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly HttpCall httpCall = new HttpCall();
        [HttpGet]
        public ActionResult Index()
        {       
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetPCIPalSession()
        {
            SessionResponse _sessionResponse = new SessionResponse();
            var RequestTokenPara = new[]
            {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("username", "APIUser"),
                    new KeyValuePair<string, string>("client_id", "ActuariesStg120722"),
                    new KeyValuePair<string, string>("tenantname", "Institute and Faculty of Actuaries"),
                    new KeyValuePair<string, string>("client_secret", "P0ub9WblSjkcfZEostYo8G0gwDP4gmZomMpNYGyW")

                };

            var GetToken = await httpCall.GetToken(RequestTokenPara);
            if (GetToken != null)
            {
                var InitalValue = new IntialValuesPara
                {
                    key = ""
                };
                var sessionReqPara = new SessionRequestPara
                {
                    FlowId = 2656,
                    InitialValues = InitalValue
                };
                 _sessionResponse = await httpCall.GetPciPalSession(sessionReqPara, GetToken.access_token);
                _sessionResponse.access_token = GetToken.access_token;
                _sessionResponse.refresh_token = GetToken.refresh_token;
            }
            return Json(_sessionResponse, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<JsonResult> GetPCIPalDisplaySession(string iframeUrl, string access_token, string refresh_token)
        {
            SessionResponse _sessionResponse = new SessionResponse();
            //var RequestTokenPara = new[]
            //{
            //        new KeyValuePair<string, string>("grant_type", "client_credentials"),
            //        new KeyValuePair<string, string>("username", "APIUser"),
            //        new KeyValuePair<string, string>("client_id", "ActuariesStg120722"),
            //        new KeyValuePair<string, string>("tenantname", "Institute and Faculty of Actuaries"),
            //        new KeyValuePair<string, string>("client_secret", "P0ub9WblSjkcfZEostYo8G0gwDP4gmZomMpNYGyW")

            //    };

            // var GetToken = await httpCall.GetToken(RequestTokenPara);
            //if (GetToken != null)
            //{
            //    var InitalValue = new IntialValuesPara
            //    {
            //        key = ""
            //    };
            //    var sessionReqPara = new SessionRequestPara
            //    {
            //        FlowId = 2656,
            //        InitialValues = InitalValue
            //    };
            //    _sessionResponse = await httpCall.GetPciPalSession(sessionReqPara, GetToken.access_token);
            //    _sessionResponse.access_token = GetToken.access_token;
            //    _sessionResponse.refresh_token = GetToken.refresh_token;
            //}

            return await httpCall.GetPciPalDisplaySession(iframeUrl, access_token, refresh_token);
            //return Json(_sessionResponse, JsonRequestBehavior.AllowGet);

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}