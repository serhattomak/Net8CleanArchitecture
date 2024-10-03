using System.Reflection;
using App.Application.Features.Categories;
using App.Application.Features.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application.Extensions;

public static class ServiceExtension
{
	public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
		services.AddScoped<IProductService, ProductService>();
		services.AddScoped<ICategoryService, CategoryService>();

		services.AddFluentValidationAutoValidation();

		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddAutoMapper(Assembly.GetExecutingAssembly());


		//TODO : MOVE TO THE API
		//services.AddScoped(typeof(NotFoundFilter<,>));
		//services.AddExceptionHandler<CriticalExceptionHandler>();
		//services.AddExceptionHandler<GlobalExceptionHandler>();

		return services;
	}
}