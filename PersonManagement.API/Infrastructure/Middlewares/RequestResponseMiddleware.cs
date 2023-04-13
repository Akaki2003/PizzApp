
using Newtonsoft.Json;
using PizzApp.Application.GlobalExceptionHandling;
using System.Text;

namespace PizzApp.API.Middlewares
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private const string LogFile = "C:\\Users\\Guram\\Desktop\\logFile.txt";
        public RequestResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stream originalBody = context.Response.Body;
            string responseBody = null;
            try
            {
                await RequestInfoLogger(context,originalBody);
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await _next(context);

                    memStream.Position = 0;
                    responseBody = new StreamReader(memStream).ReadToEnd();
                  

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }

            }
            finally
            {
                context.Response.Body = originalBody;
                await ResponseInfoLogger(context.Response, responseBody); 
            }
        }

        private async Task RequestInfoLogger(HttpContext context, Stream originalBody)
        {
            HttpRequest request = context.Request;
            
            await File.AppendAllTextAsync(LogFile,"REQUEST------> \n");

            await File.AppendAllTextAsync(LogFile,  context.Connection.RemoteIpAddress.ToString()); //ip
            await File.AppendAllTextAsync(LogFile, context.Request.Query.ToString()); //query
            await File.AppendAllTextAsync(LogFile, DateTime.Now.ToString());//time
            await File.AppendAllTextAsync(LogFile, request.Scheme); //address
            await File.AppendAllTextAsync(LogFile, request.Path); //path
            await File.AppendAllTextAsync(LogFile, request.IsHttps.ToString()); //IsHttps
            await File.AppendAllTextAsync(LogFile, ReadRequestBody(request).ToString()); //body

        }
       

        async Task ResponseInfoLogger(HttpResponse response, string body)
        {

            await File.AppendAllTextAsync(LogFile, "Response-------");
            await File.AppendAllTextAsync(LogFile, DateTime.Now.ToString());
            await File.AppendAllTextAsync(LogFile, body); 

        }


        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[request.ContentLength ?? 0];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            return bodyAsText;
        }

    }
}
