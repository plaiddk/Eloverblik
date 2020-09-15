using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ElOverblik
{
    class ElOverblikGetDataHelper
    {

        public static async Task<string> GetData(string url, string contentType,string token, string meteringPoint)
        {

            HttpClient clientprice = new HttpClient();

            clientprice.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            clientprice.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resultprice = await clientprice.PostAsync(url, new StringContent(meteringPoint, Encoding.UTF8, "application/json"));
            string resultContentPrice = await resultprice.Content.ReadAsStringAsync();
            return resultContentPrice;

        }
    }
}
