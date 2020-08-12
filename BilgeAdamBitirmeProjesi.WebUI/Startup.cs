using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Refit;

namespace BilgeAdamBitirmeProjesi.WebUI
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

            //Register Refit Interfaces
            RegisterClients(services);

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //MVC Modulunu ekled�m.
            services.AddControllersWithViews()
                //RunTime Nuget'den install edip kulland�m
                .AddRazorRuntimeCompilation();

            //Asp.Net Core MVC'de Oturum Y�netimini Core Identity ile ger�ekle�tiriyorum.Core Identityde Cookie y�netimini kullan�yorum.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                });

            //Net Core Yap�s� tamam�yla Dependency Injection yap�s� ile �al��t���ndan dolay� Interface ile Service classlar�n�n ba��ml�l���n� tan�mlad���m yer.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Geli�tirme S�resince
            if (env.IsDevelopment())
            {
                //Hata sayfalar�n� g�rmek istedi�imizden dolay� a�a��daki servisi aktifle�tirdim.
                app.UseDeveloperExceptionPage();
            }
            //Core Identity servisimizi aktifle�tirdim.
            app.UseAuthentication();
            //wwwroot klasorune eri�im izni.
            app.UseStaticFiles();
            //Durum kodlar� ile ilgili sayfalar� kullanma izni veriyorum.
            app.UseStatusCodePages();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterClients(IServiceCollection services)
        {
            //Kullan�lan her class konteynr'a eklenmeli.
            //RegisterHttpHandler
            services.AddScoped<AuthTokenHandler>();

            var baseUrl = Configuration
                .GetSection("Settings")
                .GetSection("Host")["CoreServer"];
            var baseUri = new Uri(baseUrl);

            //Account
            services.AddRefitClient<IAccountApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));

            //Category
            services.AddRefitClient<ICategoryApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
            .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

            //Product
            services.AddRefitClient<IProductApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

            //User
            services.AddRefitClient<IUserApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

            //Comment
            services.AddRefitClient<ICommentApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

            //Order
            services.AddRefitClient<IOrderApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

            services.AddRefitClient<ICartApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

            services.AddRefitClient<ICartItemApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

            services.AddRefitClient<IOrderDetail>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());



        }
    }
}
