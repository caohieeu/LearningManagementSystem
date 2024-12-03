using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LearningManagementSystem.Utils;
using System.Net;
using LearningManagementSystem.Exceptions;

public class ResponseMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        ResponseEntity response;

        switch(ex)
        {
            case SubjectNotFoundException _:
                response = new ResponseEntity()
                {
                    code = ErrorCode.NotFound.GetErrorInfo().code,
                    message = ex.Message,
                    data = Array.Empty<string>(),
                };
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case NotFoundException _:
                response = new ResponseEntity()
                {
                    code = ErrorCode.NotFound.GetErrorInfo().code,
                    message = ex.Message,
                    data = Array.Empty<string>()
                };
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case AuthorizationException _:
                response = new ResponseEntity()
                {
                    code = ErrorCode.Unauthorized.GetErrorInfo().code,
                    message = ex.Message,
                };
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case ArgumentNullException _:
                response = new ResponseEntity()
                {
                    code = 400,
                    message = ex.Message,
                };
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case LearningManagementSystem.Exceptions.ArgumentException _:
                response = new ResponseEntity()
                {
                    code = 400,
                    message = ex.Message,
                };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case AlreadyExistException _:
                response = new ResponseEntity()
                {
                    code = 422,
                    message = ex.Message,
                };
                context.Response.StatusCode = 422;
                break;
            case AuthenticationExceptionSub _:
                response = new ResponseEntity()
                {
                    code = 401,
                    message = ex.Message,
                };
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            default:
                response = new ResponseEntity()
                {
                    code = 500,
                    //message = "Internal Server Error",
                    message = ex.Message,
                };
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}
