using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CCDClient
{
    class Program
    {
        public string accesstoken = string.Empty;
        HttpClient ccdclient = new HttpClient();
        static async Task Main(string[] args)
        {
            TokenManager tknobj = new TokenManager();
            string accesstoken = await tknobj.GetToken();
            GetData startfetch = new GetData();
            startfetch.FetchAPIData(accesstoken);
        }       
    }
}
