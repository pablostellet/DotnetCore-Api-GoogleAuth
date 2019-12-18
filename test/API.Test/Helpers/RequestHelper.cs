using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace API.Test.Helpers
{
    public class RequestHelper<T>
    {
        public StringContent Data { get; set; }

        public RequestHelper(T model)
        {
            Data = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        }
    }
}