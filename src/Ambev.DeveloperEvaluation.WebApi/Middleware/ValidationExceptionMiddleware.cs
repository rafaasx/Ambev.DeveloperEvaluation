using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            var apiResponse = new ApiResponse();
            apiResponse.Success = false;
            apiResponse.Message = ex.Message;
            await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse, _jsonOptions));
        }
    }

    private Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = new ApiResponse
        {
            Success = false,
            Message = "Validation Failed",
            Errors = exception.Errors
                .Select(error => (ValidationErrorDetail)error)
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, _jsonOptions));
    }
}
