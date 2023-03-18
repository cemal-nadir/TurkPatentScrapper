using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Reflection;
using Amazon.S3;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Scrap.API.ControllerServices.Contracts;
using TPHunter.WebServices.Scrap.API.DI;
using TPHunter.WebServices.Scrap.PatentPdf.Abstract;
using TPHunter.WebServices.Scrap.PatentPdf.Concrete;
using TPHunter.WebServices.Shared.MainData.Core.Repositories;
using TPHunter.WebServices.Shared.MainData.Core.Services;
using TPHunter.WebServices.Shared.MainData.Core.UnitOfWorks;
using TPHunter.WebServices.Shared.MainData.Data;
using TPHunter.WebServices.Shared.MainData.Data.Repositories;
using TPHunter.WebServices.Shared.MainData.Data.UnitOfWorks;
using TPHunter.WebServices.Shared.MainData.Services;
using TPHunter.WebServices.Shared.Utility.FileStorage;
using TPHunter.WebServices.Shared.Utility.Core.Abstract.FileStorage;
using AmazonS3Config = TPHunter.WebServices.Shared.Utility.Core.Models.FileStorage.AmazonS3Config;

namespace TPHunter.WebServices.Scrap.API
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = Configuration.GetSection("IdentityServerURL").Value;
                options.Audience = "resource_scrap";
                options.RequireHttpsMetadata = false;
            });

            var migrationsAssembly = typeof(MainDataContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<MainDataContext>(options =>
            {
                options.UseNpgsql(Configuration["ConnectionStrings:RDS"], x => x.MigrationsAssembly(migrationsAssembly));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddOptions();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<DbContext, MainDataContext>();
            services.Configure<AmazonS3Config>(Configuration.GetSection("AmazonConfig:AmazonS3Config"));
            services.AddScoped(typeof(IAmazonS3Config), typeof(AmazonConfigFactory));
            services.AddScoped(typeof(IAmazonS3), typeof(CustomAmazonS3Client));
            //services.AddScoped(typeof(IFileTransferManager), typeof(AmazonStorage));
            services.AddScoped(typeof(IFileTransferManager), typeof(FakeStorage));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddHttpClient<IPdfDownloaderService,PdfDownloaderService>(client =>
                client.Timeout = TimeSpan.FromMinutes(10));
            services.AddScoped(typeof(IControllerService<MarkModel>), typeof(TradeMarkService));
            services.AddScoped(typeof(IControllerService<PatentModel>), typeof(PatentService));
            services.AddScoped(typeof(IControllerService<DesignModel>), typeof(DesignService));
            services.AddControllers();

         
          

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TPHunter.WebServices.Scrap.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Apiyi kullanabilmek için token ile auth olmanýz gerekmektedir." +
      "Metin kutusuna önce \'Bearer\' sonrasýnda bir boþluk býrakarak keyi yazýn." +
      "Örnek: \'Bearer 12345abcdef\'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header

          },
            new List<string>()
          }
        });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TPHunter.WebServices.Scrap.API v1"));
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
