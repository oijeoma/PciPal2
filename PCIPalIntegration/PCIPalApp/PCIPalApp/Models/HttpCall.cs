using PCIPalApp.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PCIPalApp.Models
{
    public class HttpCall : IHttpCall
    {
        public async Task<Response> GetToken(KeyValuePair<string, string>[] RequestPara)
        {
            MemoryStream ms = null;
            Response _response = null;
            try
            {
                _response = new Response();
                DataContractJsonSerializerSettings setting = new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true };
                DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(Response), setting);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://pcipalstaging.cloud/");
                    string connUrl = "api/v1/token";
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    HttpResponseMessage _responseData = client.PostAsync(connUrl, new FormUrlEncodedContent(RequestPara)).Result;
                    if (_responseData.IsSuccessStatusCode)
                    {
                        var responseResult = await _responseData.Content.ReadAsStringAsync();

                        using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(responseResult)))
                        {
                            _response = (Response)jsonSer.ReadObject(stream);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //  Log.Information("Error while Rating API");
                throw ex;
            }
            return await Task.Run(() => _response);


        }

        public async Task<SessionResponse> GetPciPalSession(SessionRequestPara RequestPara, string access_token)
        {
            MemoryStream ms = null;
            SessionResponse _response = null;
            try
            {
                _response = new SessionResponse();
                DataContractJsonSerializerSettings setting = new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true };
                DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(SessionResponse), setting);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://euwest1.pcipalstaging.cloud/");
                    string connUrl = "api/v1/session/617/launch";
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var result = string.Empty;
                    using (ms = new MemoryStream())
                    {
                        jsonSer.WriteObject(ms, RequestPara);
                        result = Encoding.Default.GetString(ms.ToArray());
                        result = "{" + result.Substring(49);
                        ms.Position = 0;
                        StreamReader sr = new StreamReader(ms);
                        sr.ReadToEnd();
                        ms.Close();
                    }
                    HttpContent content = new StringContent(result, Encoding.Unicode, "application/json");
                    HttpResponseMessage _responseData = client.PostAsync(connUrl, content).Result;

                    if (_responseData.IsSuccessStatusCode)
                    {
                        var responseResult = await _responseData.Content.ReadAsStringAsync();

                        using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(responseResult)))
                        {
                            _response = (SessionResponse)jsonSer.ReadObject(stream);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //  Log.Information("Error while Rating API");
                throw ex;
            }
            return await Task.Run(() => _response);


        }


        public async Task<JsonResult> GetPciPalDisplaySession(string iframeUrl, string access_token, string refresh_token)
        {
            MemoryStream ms = null;
            var response = "";
            try
            {
                
                DataContractJsonSerializerSettings setting = new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true };
                DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(SessionResponse), setting);

                using (var client = new HttpClient())
                {
                   
                    client.BaseAddress = new Uri(iframeUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    
                    var result = string.Empty;
                   
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                            new KeyValuePair<string, string>("x-bearer-token", access_token),
                            new KeyValuePair<string, string>("x-refresh-token", refresh_token)
                    });
                   
                    HttpResponseMessage _responseData = client.PostAsync(client.BaseAddress, formContent).Result;                   
                    if (_responseData.IsSuccessStatusCode)
                    {
                        var responseResult = await _responseData.Content.ReadAsStringAsync();                       
                        response = responseResult;                     
                    }


                }
            }
            catch (Exception ex)
            {
                //  Log.Information("Error while Rating API");
                throw ex;
            }
            return new JsonResult { Data = response,MaxJsonLength = Int32.MaxValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
          


        }


    }

}