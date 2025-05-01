using Microsoft.AspNetCore.Mvc;
using TalabatCore;
using TalabatPL.Erros;
using TalabatPL.Helpers;
using TalabatRepository;

namespace TalabatPL.Extenstions
{
    public static class AddApplicationServices
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(e => e.AddProfile(new MappingProfiles()));
            services.AddTransient<ProductPictureResolver>();

            #region Handling Validation Errors

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(e => e.Value.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Erros = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            #endregion
            return services;
        }
    }
}
