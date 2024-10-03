using CleanApp.API.ExceptionHandler;

namespace CleanApp.API.Extensions;

public static class ExceptionHandlerExtensions
{
	public static IServiceCollection AddExceptionHandlerExt(this IServiceCollection services)
	{

		services.AddExceptionHandler<CriticalExceptionHandler>();
		services.AddExceptionHandler<GlobalExceptionHandler>();

		return services;
	}
}