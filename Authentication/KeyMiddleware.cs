using Authentication.Models;
using Microsoft.AspNetCore.Http;

namespace Authentication
{
    public class KeyMiddleware
    {
        private RequestDelegate next;

        public KeyMiddleware(RequestDelegate next )
        {
            this.next = next;
        }

        public IUserManger userManger { get; set; } 

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //await  httpContext.Response.WriteAsync("Hello Farid!");
            //var key = httpContext.Request.Query["key"];
            //if (key=="qwerty")
            //{
            //    await next.Invoke(httpContext);
            //}
            //else
            //{
            //    await httpContext.Response.WriteAsync("Not key!");
            //}

            var userManger =  httpContext.RequestServices.GetRequiredService<IUserManger>();
            var user = userManger.GetUserCredentials();
            if (user != null) await next.Invoke(httpContext);
            else
            {
                await httpContext.Response.WriteAsync("UNAUTHORIZED!!!!");

            }

        }
    }
}
