
using Grad.Models;
using Grad.Repo.Base;
using Grad.Repo;
using Microsoft.EntityFrameworkCore;

namespace Grad
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });
            builder.Services.AddTransient(typeof(IRepoBase<>), typeof(MainRepo<>));
            builder.Services.AddDistributedMemoryCache(); // Add in-memory cache
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("pol", policy => policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseCors("pol");
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
