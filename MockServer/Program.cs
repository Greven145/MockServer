using System;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

WebHost.Start(routes => {
    async Task Handler(HttpRequest req, HttpResponse res, RouteData data) {
        var filePath = $"content/{data.Values["slug"]}-{req.Method.ToLower()}.json";
        if (File.Exists(filePath)) {
            await res.WriteAsJsonAsync(JsonConvert.DeserializeObject<ExpandoObject>(await File.ReadAllTextAsync(filePath), new ExpandoObjectConverter()));
            return;
        }
        res.StatusCode = (int)HttpStatusCode.NotFound;
    }
    routes.MapRoute("/api/{**slug}", context => Handler(context.Request, context.Response, context.GetRouteData()));
});
Console.ReadKey();