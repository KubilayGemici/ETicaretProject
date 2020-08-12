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
            //Ana root dizini verdim.Appsettings.json yakalayabilmek için.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", reloadOnChange: true, optional: true)
                //O anda hangi enviroment ile çalýþýyorsam izin verilecek.
                .AddJsonFile($"appsettings{env.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();
            //Tüm ayarlarý Configuration'da toplamama yardýmcý olucak.
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            //Add HttpContextAccessor, ayaða kaldýrdým.
            services.AddHttpContextAccessor();

            //Register Refit Interfaces
            RegisterClients(services);

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //MVC Modulunu ekledým.
            services.AddControllersWithViews()
                //RunTime Nuget'den install edip kullandým
                .AddRazorRuntimeCompilation();

            //Asp.Net Core MVC'de Oturum Yönetimini Core Identity ile gerçekleþtiriyorum.Core Identityde Cookie yönetimini kullanýyorum.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                });

            //Net Core Yapýsý tamamýyla Dependency Injection yapýsý ile çalýþtýðýndan dolayý Interface ile Service classlarýnýn baðýmlýlýðýný tanýmladýðým yer.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Geliþtirme Süresince
            if (env.IsDevelopment())
            {
                //Hata sayfalarýný görmek istediðimizden dolayý aþaðýdaki servisi aktifleþtirdim.
                app.UseDeveloperExceptionPage();
            }
            //Core Identity servisimizi aktifleþtirdim.
            app.UseAuthentication();
            //wwwroot klasorune eriþim izni.
            app.UseStaticFiles();
            //Durum kodlarý ile ilgili sayfalarý kullanma izni veriyorum.
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
            //Kullanýlan her class konteynr'a eklenmeli.
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
