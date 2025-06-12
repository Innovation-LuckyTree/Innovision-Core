using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Innovision.Core.Infrastructure.Models;

public class ApiException : HttpRequestException
{
    public ApiException(string api, string body, string? message, Exception? inner, HttpStatusCode? statusCode)
        : base(message, inner, statusCode)
        => (Api, Body) = (api, body);

    public string Api { get; private set; }
    public string Body { get; private set; }
}