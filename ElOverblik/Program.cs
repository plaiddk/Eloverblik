using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ElOverblik
{
    class Program
    {
        public static string contentType = "application/json";

        public static string meteringPoint = @"{""meteringPoints"": {""meteringPoint"": [ ""INDSÆT DIT MÅLENUMMER"" ] }}";
        public static async Task Main(string[] args)
        {

            //Get Token for Eloverblik.dk
            string tokenUrl = "https://api.eloverblik.dk/CustomerApi/api/Token";
            string token = "INDSÆT TOKEN FRA Eloverblik.dk";
            string eloverblikAccess = await InitializeToken(tokenUrl, token);

            //Get prices from Eloverblik.dk            
            string eloverblikPrisUrl = "https://api.eloverblik.dk/CustomerApi/api/MeteringPoints/MeteringPoint/GetCharges";
            string prisdata = await ElOverblikGetDataHelper.GetData(eloverblikPrisUrl, contentType, eloverblikAccess, meteringPoint);
            File.WriteAllText(@"D:\testdata\prisdata.json", prisdata);

            //Get Metering data from Eloverblik.dk
            string eloverblikMeteringUrl = "https://api.eloverblik.dk/CustomerApi/api/MeterData/GetTimeSeries";
            string fromdate = "2018-01-01";
            string todate = DateTime.Now.ToString("yyyy-MM-dd");
            string Aggregation = "Hour";
            string eloverblikMeteringFullUrl = eloverblikMeteringUrl + $"/{fromdate}/{todate}/{Aggregation}";

            string meteringData = await ElOverblikGetDataHelper.GetData(eloverblikMeteringFullUrl, contentType, eloverblikAccess, meteringPoint);
            File.WriteAllText(@"D:\testdata\meteringdata.json", meteringData);

        }

        public static async Task<string> InitializeToken(string _url, string _token)
        {
            var elToken = await ElOverblikTokenHelper.GetToken(_url, _token);

            JObject tmp = JObject.Parse(elToken);
            string tokenAccess = tmp["result"].ToString();

            return tokenAccess;
        }
    }
}
