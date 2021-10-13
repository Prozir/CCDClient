using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CCDClient
{
    class TokenManager
    {
        public async Task<string> GetToken()
        {
            HttpClient ccdclient = new HttpClient();
            string accesstoken = string.Empty;
            string ccdtokenurl = ConfigurationManager.AppSettings["ccdtokenurl"];
            string clientId = ConfigurationManager.AppSettings["ccdclientid"];
            string clientSecret = ConfigurationManager.AppSettings["ccdclientsecret"];
            HttpRequestMessage tokenmessage = new HttpRequestMessage(HttpMethod.Post, ccdtokenurl);

            //adding body parameters in dictionary
            Dictionary<string, string> tokenbody = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "resource", ConfigurationManager.AppSettings["ccdtokenresource"]},
                { "client_secret", clientSecret },
                { "grant_type", "client_credentials" }
            };

            var content = new FormUrlEncodedContent(tokenbody);
            tokenmessage.Content = content;
            tokenmessage.Headers.Add("Host", "login.microsoftonline.com");
            var tokenresponse = await ccdclient.SendAsync(tokenmessage);
            string ccdtokenresponse = await tokenresponse.Content.ReadAsStringAsync();
            JObject jobj = JObject.Parse(ccdtokenresponse);
            accesstoken = (string)jobj["access_token"]; // get the token from response                                                                
            return accesstoken;
        }
    }
}
