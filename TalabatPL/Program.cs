
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TalabatCore;
using TalabatPL.Helpers;
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

            var app = builder.Build();

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
