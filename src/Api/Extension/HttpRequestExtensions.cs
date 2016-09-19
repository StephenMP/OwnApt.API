using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Extension
{
    public static class HttpRequestExtensions
    {
        public static string GetResourcePathSafe(this HttpRequest request)
        {
            return request == null ? "" : $"{request.Host}{request.Path}";
        }

        public static string GetResourcePathSafe(this HttpRequest request, string id)
        {
            return request == null ? "" : $"{request.Host}{request.Path}/{id}";
        }
    }
}
