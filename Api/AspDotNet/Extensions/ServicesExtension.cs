using System;
using System.Globalization;
using AspDotNet.Filters;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace AspDotNet.Extensions;

public static class ServicesExtension
{
    public static void ConfigureLogging(this IServiceCollection service_collection, IConfiguration configuration) =>
        service_collection.AddSingleton<ILogger>(_ =>
            new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", configuration.GetValue<string>("Environment"))
                .WriteTo.Console()
                .WriteTo.Sink(new MSSqlServerAuditSink(configuration.GetConnectionString("tickers"),
                    new MSSqlServerSinkOptions()
                    {
                        SchemaName = "dbo",
                        TableName = "t_Logs",
                        AutoCreateSqlTable = true,
                        BatchPeriod = TimeSpan.FromSeconds(10),
                        BatchPostingLimit = 1000,
                        EagerlyEmitFirstEvent = true,
                        UseAzureManagedIdentity = false
                    },
                    CultureInfo.GetCultureInfo("el-GR"),
                    new ColumnOptions()
                    {
                        PrimaryKey = { ColumnName = "id" },
                        Id = { ColumnName = "id" },
                        ClusteredColumnstoreIndex = true,
                        Level = { ColumnName = "level" },
                        LogEvent = { ColumnName = "event" },
                        Exception = { ColumnName = "exception" },
                        Message = { ColumnName = "message" },
                        Properties = { ColumnName = "properties" },
                        TimeStamp = { ColumnName = "create_date" }
                    }))
                .CreateLogger());

    public static void ConfigureCors(this IServiceCollection service_collection) =>
        service_collection.AddCors(options =>
            options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins(
                        "https://api.tickers.gr",
                        "https://sales.tickers.gr",
                        "https://api.tickers.gr:5001",
                        "https://sales.tickers.gr:5001")
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

    public static void ConfigureSqlContext(this IServiceCollection service_collection, IConfiguration configuration)
    {
        service_collection.AddDbContext<Context>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("tickers"), builder =>
                builder.MigrationsAssembly(nameof(AspDotNet))));
    
        service_collection.AddDbContext<Users.Permissions.Database.Context>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("tickers")));
         service_collection.AddDbContext<Users.Roles.Database.Context>(opts =>
             opts.UseSqlServer(configuration.GetConnectionString("tickers")));
          service_collection.AddDbContext<Users.Database.Context>(opts =>
              opts.UseSqlServer(configuration.GetConnectionString("tickers")));
         service_collection.AddDbContext<Events.Database.Context>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("tickers")));
    }

    public static void ConfigureFilters(this IServiceCollection service_collection)
    {
        service_collection.AddScoped<ValidationFilterAttribute>();
        service_collection.AddScoped<MediaTypeAttribute>();
    }

    public static void ConfigureRepositories(this IServiceCollection service_collection)
    {
        service_collection.AddScoped<Users.Interfaces.IRepository, Users.Database.Repository>();
        service_collection.AddScoped<Users.Roles.Interfaces.IRepository, Users.Roles.Database.Repository>();
        service_collection.AddScoped<Users.Permissions.Interfaces.IRepository, Users.Permissions.Database.Repository>();
        service_collection.AddScoped<Events.Interfaces.IRepository, Events.Database.Repository>();
    }

    public static void ConfigureiFastEndpoints(this IServiceCollection service_collection, IConfiguration configuration)
    {
        service_collection.AddFastEndpoints();
        service_collection.AddAuthenticationJWTBearer(configuration.GetValue<string>("JWT:secret"));
    }

    public static void ConfigureVersioning(this IServiceCollection service_collection)
    {
        service_collection.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });
    }

    public static void ConfigureSwagger(this IServiceCollection service_collection) =>
        service_collection.AddSwaggerDoc(settings =>
        {
            settings.DocumentName = "Initial Release";
            settings.Title = "Tickers";
            settings.Version = "v1.0";
        })
        .AddSwaggerDoc(maxEndpointVersion: 1, settings: settings =>
        {
            settings.DocumentName = "Release 1.0";
            settings.Title = "Tickers";
            settings.Version = "v1.0";
        });

    public static void ConfigureApiServices(this IServiceCollection service_collection, IConfiguration configuration)
    {
        service_collection.AddSingleton<Users.Interfaces.IJwtService, Users.Services.JwtService>();
        service_collection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        service_collection.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); 
    }
}
