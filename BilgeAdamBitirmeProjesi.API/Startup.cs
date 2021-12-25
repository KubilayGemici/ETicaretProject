using AutoMapper;
using BilgeAdamBitirmeProjesi.API.Infrastructure.Models.Base;
using BilgeAdamBitirmeProjesi.Common.Client.Services;
using BilgeAdamBitirmeProjesi.Model.Context;
using BilgeAdamBitirmeProjesi.Service.Service.Cart;
using BilgeAdamBitirmeProjesi.Service.Service.CartItem;
using BilgeAdamBitirmeProjesi.Service.Service.Category;
using BilgeAdamBitirmeProjesi.Service.Service.Comment;
using BilgeAdamBitirmeProjesi.Service.Service.Order;
using BilgeAdamBitirmeProjesi.Service.Service.OrderDetail;
using BilgeAdamBitirmeProjesi.Service.Service.Product;
using BilgeAdamBitirmeProjesi.Service.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.API
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IWebHostEnvironment env)
        {
            //Ana root dizini verdim.Appsettings.json yakalayabilmek i�in.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", reloadOnChange: true, optional: true)
                //O anda hangi enviroment ile �al���yorsam izin verilecek.
                .AddJsonFile($"appsettings{env.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();
            //T�m ayarlar� Configuration'da toplamama yard�mc� olucak.
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            //Add HttpContextAccessor, aya�a kald�rd�m.
            services.AddHttpContextAccessor();

            //API Mod�l�n� s�recime ekledim.
            services.AddControllers();

            //AutoMapper Ekleme K�sm�
            services.AddAutoMapper(typeof(Startup));

            //DataContext g�nderilecek Postgre SQL connection string yap�s�n� olu�turdum.
            services.AddDbContext<DataContext>(option =>
            {
                option.UseNpgsql(Configuration["ConnectionStrings:Conn"]);
            });

            //Net Core Yap�s� tamam�yla Dependency Injection yap�s� ile �al��t���ndan dolay� Interface ile Service classlar�n�n ba��ml�l���n� tan�mlad���m yer.
            //Mulakatlarda AddSingleton - AddScoped - AddTransient ��kar.!
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IWorkContext, ApiWorkContext>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICartItemService, CartItemService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderDetailService, OrderDetailService>();


            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
                options.AddPolicy("AllowWithOrigins",
                    b => b.WithOrigins(
                        "http://localhost:5000"
                        )
                         .AllowAnyHeader()
                         .AllowAnyMethod());
            });

            //Aut eklemesi yap�ld�.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };
                });
            //Swagger eklemesi
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Swagger on ASP.NET",
                    Version = "1.0.0",
                    Description = "R�ya Ev Backend Servis(ASP.NET Core)",
                    TermsOfService = new System.Uri("http://swagger.io/terms")
                });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "R�ya Ev Core API projesi JWT Authorization header (Bearer) kullan�lmaktad�r. \"Authorization: Bearer {token}\"",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                           Reference = new OpenApiReference{
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                           }
                        }, new List<string>()
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "MyAPI");
                    c.RoutePrefix = "swagger";
                });
            }
            app.UseCors("AllowAll");
            app.UseRouting();
            //Aut Aya�� kald�rma i�lemi yapt�m.
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Map kontrollerimizi ekledik.
                endpoints.MapControllers();
            });
        }
    }
}
