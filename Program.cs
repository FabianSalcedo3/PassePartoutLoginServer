using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PassePartoutApi.data_access;
using PassePartoutApi.Infrastracture;

namespace PassePartoutApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            //Aggiungo il mio DbContext e gli associo la Default connection dell' appsettings.json (da modificare per test)
            builder.Services.AddDbContext<UtenteDbContext>(options => options.UseSqlServer( 
                builder.Configuration.GetConnectionString("DefaultDbConnection")));

            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}