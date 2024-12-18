using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.Run(async(HttpContext httpContext) => {

    var path = httpContext.Request.Path;
    var methodName = httpContext.Request.Method;
    if (path == "/" || path == "/Home")
    {
        httpContext.Response.StatusCode = 200;
        await httpContext.Response.WriteAsync("hi zain this is your first app");
    }
    else if (methodName == "GET" && path == "/Product")
    {
        httpContext.Response.StatusCode = 200;
        if (httpContext.Request.Query.ContainsKey("id")
        && httpContext.Request.Query.ContainsKey("productName"))
        {
            var id = httpContext.Request.Query["id"];
            var productName = httpContext.Request.Query["productName"];
            await httpContext.Response.WriteAsync("Hi, You are in Product page. " +
                "Your Selected Product is " + id + " and Product Name is " + productName);
        }
        await httpContext.Response.WriteAsync("Hi, You are in Product page.");
    }
    else if (methodName == "POST" && path == "/Product")
    {
        //StreamReader reader = new StreamReader(httpContext.Request.Body);
        //var data=await reader.ReadToEndAsync();
        //httpContext.Response.StatusCode = 200;
        //await httpContext.Response.WriteAsync("Product Page,The data body contains is:"+data);

        var id = "";
        var productName = "";
        StreamReader reader = new StreamReader(httpContext.Request.Body);
        var data = await reader.ReadToEndAsync();
        Dictionary<string,StringValues> dict=QueryHelpers.ParseQuery(data);
        if (dict.ContainsKey("id"))
        {
            id=dict["id"];        
        }
        if (dict.ContainsKey("productName"))
        {
            foreach (var dt in dict["productName"])
            {
                productName = productName+"" + dt+",";
            }
            
        }
        httpContext.Response.StatusCode = 200;
        await httpContext.Response.WriteAsync("Product Page," +
            "The data body contains is ID:" + id+", ProductName : "+productName);


    }
    else if (path == "/Contact")
    {
        httpContext.Response.StatusCode = 200;
        await httpContext.Response.WriteAsync("Hi, You are in Contact-Us page.");
    }
    else if (path == "/About")
    {
        httpContext.Response.StatusCode = 200;
        await httpContext.Response.WriteAsync("Hi, You are in About-Us page.");
    }
    else
    {
        httpContext.Response.StatusCode = 404;
        await httpContext.Response.WriteAsync("Hi, The page you are looking for is not found.");
    }

    return;
});
app.Run();
