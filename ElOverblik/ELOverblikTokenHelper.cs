using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ElOverblik
{
    public class ElOverblikTokenHelper
    {
        public static async Task<string> GetToken(string tokenUrl,string eloverblikToken)
        {
            
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", eloverblikToken);
            HttpResponseMessage res = await client.GetAsync(tokenUrl);
            res.EnsureSuccessStatusCode();
            var bodycontent = await res.Content.ReadAsStringAsync();
            
            return bodycontent;
        }

    }
}
