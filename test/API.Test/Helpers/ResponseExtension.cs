using System.Net.Http;
using API.Entities;
using Newtonsoft.Json;

namespace API.Test.Helpers
{
    public static class ResponseExtension
    {
        public static string Data(this HttpResponseMessage response)
            => response.Content.ReadAsStringAsync().Result;

        public static UserTokenVm ToUserToken(this string json)
        {
            return JsonConvert.DeserializeObject<UserTokenVm>(json);
        }
    }
}