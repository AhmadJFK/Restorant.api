
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Restorant.Core.Common;
using Restorant.Core.Repositories;
using Restorant.Core.Services;
using Restorant.Infra.Common;
using Restorant.Infra.Repositories;
using Restorant.Infra.Services;

namespace Restorant.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
            {
                         options.TokenValidationParameters = new TokenValidationParameters
                {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Hello Everbody,Hello Everbody,Hello Everbody")),
                         ClockSkew = TimeSpan.Zero
                };
            }); // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDBContext, DBContext>();
            builder.Services.AddScoped<ICategory_Repository, Category_Repository>();
            builder.Services.AddScoped<ICustomers_Repository, Customers_Repository>();
            builder.Services.AddScoped<IEmployee_Repository, Employee_Repository>();
            builder.Services.AddScoped<IOrders_Repository, Orders_Repository>();
            builder.Services.AddScoped<ILogin_Repository, Login_Repository>();
            builder.Services.AddScoped<ICustomer_Service, Customer_Service>();
            builder.Services.AddScoped<ICategory_Service, Category_Service>();
            builder.Services.AddScoped<IOrders_Service, Orders_Service>();
            builder.Services.AddScoped<IEmployee_Service, Employee_Service>();
            builder.Services.AddScoped<ILogin_Service, Login_Service>();
            builder.Services.AddCors(c =>
            {
                c.AddPolicy("x", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("x");

            app.MapControllers();

            app.Run();
        }
    }
}
