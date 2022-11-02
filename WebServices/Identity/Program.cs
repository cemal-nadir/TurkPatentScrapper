// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;

namespace TPHunter.WebServices.Identity.API
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                // uncomment to write to Azure diagnostics stream
                //.WriteTo.File(
                //    @"D:\home\LogFiles\Application\identityserver.txt",
                //    fileSizeLimitBytes: 1_000_000,
                //    rollOnFileSizeLimit: true,
                //    shared: true,
                //    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                 
                    var persistentDbContext = serviceProvider.GetRequiredService<PersistedGrantDbContext>();
                    var configurationDbContext = serviceProvider.GetRequiredService<ConfigurationDbContext>();
                   
                    persistentDbContext.Database.Migrate();
                    configurationDbContext.Database.Migrate();
                 
                    if (!configurationDbContext.Clients.Any()||configurationDbContext.Clients.Count()<Config.Clients.Count())
                    {
                        var removes = configurationDbContext.Clients.ToList();
                        configurationDbContext.Clients.RemoveRange(removes);
                        configurationDbContext.SaveChanges();

                        foreach(var client in Config.Clients.ToList())
                        {
                            configurationDbContext.Clients.Add(client.ToEntity());
                        }
                        configurationDbContext.SaveChanges();
                    }
                    //if (configurationDbContext.IdentityResources.Count() < Config.IdentityResources.Count())
                    //{
                    //    var removes = configurationDbContext.IdentityResources.ToList();
                    //    configurationDbContext.IdentityResources.RemoveRange(removes);
                    //    configurationDbContext.SaveChanges();

                    //    foreach (var resource in Config.IdentityResources.ToList())
                    //    {
                    //        configurationDbContext.IdentityResources.Add(resource.ToEntity());
                    //    }
                    //    configurationDbContext.SaveChanges();
                    //}
                    if (configurationDbContext.ApiResources.Count() < Config.ApiResources.Count())
                    {
                        var removes = configurationDbContext.ApiResources.ToList();
                        configurationDbContext.ApiResources.RemoveRange(removes);
                        configurationDbContext.SaveChanges();

                        foreach (var resource in Config.ApiResources.ToList())
                        {
                            configurationDbContext.ApiResources.Add(resource.ToEntity());
                        }
                        configurationDbContext.SaveChanges();
                    }
                    if (configurationDbContext.ApiScopes.Count() < Config.ApiScopes.Count())
                    {
                        var removes = configurationDbContext.ApiScopes.ToList();
                        configurationDbContext.ApiScopes.RemoveRange(removes);
                        configurationDbContext.SaveChanges();

                        foreach (var apiscope in Config.ApiScopes.ToList())
                        {
                            configurationDbContext.ApiScopes.Add(apiscope.ToEntity());
                        }
                        configurationDbContext.SaveChanges();
                    }
                }
                Log.Information("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}