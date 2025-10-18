
using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Presistence;
using Store.Presistence.Data.Contexts;
using System.Threading.Tasks;

namespace Store.Web
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
            builder.Services.AddDbContext<StoreDbContext>(options => {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDbInitializer , DbInitializer>();

            var app = builder.Build();

          using var scope= app.Services.CreateScope();
           var dbInitializer= scope.ServiceProvider.GetRequiredService<IDbInitializer>();//ask clr to create object from IDbInitializer
          await  dbInitializer.InitializeAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
