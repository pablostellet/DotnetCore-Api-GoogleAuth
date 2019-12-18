using API.Controllers;

namespace API.Test.Helpers
{
    public static class ApiRoutes
    {
        public static class Example
        {
            public static string endpoint = "example/";
            public static string GetAll = endpoint;
            public static string Authenticate = endpoint + "authenticate";
        }
    }
}