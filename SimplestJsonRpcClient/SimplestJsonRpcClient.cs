using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimoItProjects
{
    public class SimplestJsonRpcClient
    {

        private bool debug = false;
        private string debug_path;

        private string url;
        private string user;
        private string password;
        private HttpClient client;


        public SimplestJsonRpcClient(string url, string user = null, string password = null)
        {
            this.url = url;
            this.user = user;
            this.password = password;
            client = new HttpClient();

            //AUTH
            var authByteArray = Encoding.ASCII.GetBytes(user + ":" + password);
            var authString = Convert.ToBase64String(authByteArray);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

            //HEADERS
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
        }

        private void LOG(string msg)
        {
            if(debug)
            {
                //string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                File.AppendAllText(debug_path + @"\SimplestJsonRpcClient.log", msg + Environment.NewLine);
            }
        }

        public void Debug(bool val, string debug_path = null)
        {
            this.debug = val;
            this.debug_path = debug_path;
            if (debug)
            {
                LOG("DEBUG ENABLED");
            }
            
        }

        public async Task<string> PostRaw(string request_json)
        {
            var request_content = new StringContent(request_json, Encoding.UTF8, "application/json");
            LOG("POST : " + request_content);
            var response = await client.PostAsync(url, request_content);
            var responseString = await response.Content.ReadAsStringAsync();

            LOG("RESPONSE : " + responseString);
            return responseString;
        }

        public async Task<object> Post(string request_json)
        {
            var request_content = new StringContent(request_json, Encoding.UTF8, "application/json");
            LOG("POST : " + request_content);
            var response = await client.PostAsync(url, request_content);
            var responseString = await response.Content.ReadAsStringAsync();
            LOG("RESPONSE : " + responseString);
            Object responseObject = JsonConvert.DeserializeObject<Object>(responseString);

            return responseObject;
        }


    }
}