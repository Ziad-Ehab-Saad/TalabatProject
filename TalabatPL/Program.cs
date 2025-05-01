
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TalabatCore;
using TalabatPL.Erros;
using TalabatPL.Helpers;
using TalabatPL.Middlewares;
using TalabatRepository;
using TalabatRepository.Data;

namespace TalabatPL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(e => e.AddProfile(new MappingProfiles()));
            builder.Services.AddTransient<ProductPictureResolver>();

            #region Handling Validation Errors
            builder.Services.Configure<ApiBehaviorOptions>(options =>
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
            var app = builder.Build();
            app.UseMiddleware<ExceptionResponseMiddleware>();
            //ask CLR explicitly to create obj from Context
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbcontext = services.GetRequiredService<StoreContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(dbcontext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
