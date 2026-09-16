using FluentValidation;

namespace MyGame.Exceptions;

public class ValidationExceptionMiddleware
{
  private readonly RequestDelegate _next;

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
      context.Response.StatusCode = 400;

      await context.Response.WriteAsJsonAsync(new
      {
        errors = ex.Errors.Select(e => new
        {
          field = e.PropertyName,
          message = e.ErrorMessage
        })
      });
    }
  }
}
