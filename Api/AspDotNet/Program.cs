global using FastEndpoints;
global using FastEndpoints.Validation;

using System.IO;
using AspDotNet;
using AspDotNet.Extensions;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder();
var configuration = builder.Configuration;

builder.Services.ConfigureLogging(configuration);
// builder.Services.ConfigureIText(configuration);
// builder.Services.ConfigureCors();
builder.Services.ConfigureSqlContext(configuration);
builder.Services.ConfigureRepositories();
// builder.Services.ConfigureFilters();
builder.Services.ConfigureiFastEndpoints(configuration);
// builder.Services.ConfigureVersioning();
// builder.Services.ConfigureResponseCaching();
// builder.Services.ConfigureHttpCacheHeaders();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureApiServices(configuration);

// builder.Services.Configure<ApiBehaviorOptions>(options =>
//     options.SuppressModelStateInvalidFilter = true);

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();
app.configureExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
    RequestPath = new PathString("/static-files")
});
// app.UseCors("CorsPolicy");
// app.UseForwardedHeaders(new ForwardedHeadersOptions
// {
// ForwardedHeaders = ForwardedHeaders.All
// });
// app.UseResponseCaching();
// app.UseHttpCacheHeaders();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(config =>
{
    config.VersioningOptions = options => options.Prefix = "v";
});
app.UseOpenApi();
app.UseSwaggerUi3(swagger_ui_options =>
    swagger_ui_options.ConfigureDefaults());
app.Run();
