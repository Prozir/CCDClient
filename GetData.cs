using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDClient
{
    class GetData
    {
        string[] allvaluesarr;
        List<string> templist = new List<string>();
        public void FetchAPIData(string accesstoken)
        {            
            var client = new RestClient(ConfigurationManager.AppSettings["ccdgetdataurl"]);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);            
            request.AddHeader("Authorization","Bearer "+ accesstoken);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            JObject getitemsobj = JObject.Parse(response.Content);

            //get all the values from response json
            var allvalues = getitemsobj["value"];
            
            for (int i = 0; i < allvalues.Count(); i++)//loop through all values 
            {
                string currdataareaid = allvalues[i]["dataAreaId"].ToString();
                //string currstatus = allvalues[i]["Status"].ToString();
                //get all the other required values like this                
                templist.Add(currdataareaid);               
            }
            allvaluesarr = templist.ToArray();
            DataSQLManager.PushDataToSQL(allvaluesarr);
        }
}
}
