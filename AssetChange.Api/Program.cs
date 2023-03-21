using AssetChange.Infra.Data.Contexts;
using AssetChange.Infra.Data.Repositories;
using AssetChange.Infra.Data.Repositories.Interfaces;
using AssetChange.Service.Services;
using AssetChange.Service.Services.External;
using AssetChange.Service.Services.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

DependenceInjectionSetup(builder);

SwaggerSetup(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SwaggerSetup(WebApplicationBuilder builder)
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(
            "v1"
            , new OpenApiInfo
            {
                Title = "AssetChange.Api",
                Version = "v1",
                Description = "AssetChange api.",
                Contact = new OpenApiContact
                {
                    Name = "Misael C. Homem",
                    Url = new Uri("https://www.github.com/mchomem")
                },
            });

        string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
}

static void DependenceInjectionSetup(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<AssetChangeContext, AssetChangeContext>();
    builder.Services.AddScoped<IAssetRepository, AssetRepository>();
    builder.Services.AddScoped<IAssetTradingDateRepository, AssetTradingDateRepository>();
    builder.Services.AddScoped<IAssetService, AssetService>();
    builder.Services.AddScoped<IAssetTradingDateService, AssetTradingDateService>();
    builder.Services.AddScoped(typeof(YahooFinanceService));
}
