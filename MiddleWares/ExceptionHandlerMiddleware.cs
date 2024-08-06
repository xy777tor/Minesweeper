using StudioTgTest.Exceptions;
using StudioTgTest.Models.DTO;

namespace StudioTgTest.MiddleWares;

public static class ExceptionHandlerMiddleware
{
    public static WebApplication UseExceptionMiddleware(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            try
            {
                await next(context);
            }
            catch (GameException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new ErrorResponse(ex.Message));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ErrorResponse("Технические шоколадки"));
            }
        });
        return app;
    }
}
