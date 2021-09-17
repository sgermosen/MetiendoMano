using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Web.Core.Contracts;
using WebPage.Helpers;
using WebPage.Helpers.Validators;
using WebPage.Hubs;
using WebPage.Services;

namespace WebPage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllersWithViews()
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<AudioFromCreateViewModelValidator>());

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin();
                }));

            services.AddSignalR();

            services.AddTransient(typeof(IDataRepository<>), typeof(DataRepository<>));
            services.AddTransient<ISpeechRecognition, SpeechRecognition>();
            services.AddTransient<AzureCognitiveService>();
            services.AddTransient<IBMWatsonServices>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseMiddleware<ExceptionHandler>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Audio}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "allLinks",
                    pattern: "{*a}",
                    defaults: new { controller = "Audio", action = "PageNotFound" });

                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
