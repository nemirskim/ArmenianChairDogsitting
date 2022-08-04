using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business;
using ArmenianChairDogsitting.Business.Exceptions;
using System.Net;

namespace ArmenianChairDogsitting.API.CustomExceptionMiddleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException error)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, error.Message);
        }
        catch (ForbiddenException error)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.Forbidden, error.Message);
        }
        catch (Exception error)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, error.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = message
        }.ToString());
    }
}
