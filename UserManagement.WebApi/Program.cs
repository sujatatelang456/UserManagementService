using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UserManagement.Application.Services;
using UserManagement.Infrastructure.Data;
using UserManagement.WebApi.Config;
using UserManagement.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DB Context
// builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

// configure dependencies using autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModule()));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CamundaService>();
builder.Services.AddScoped<AssetService>();
builder.Services.AddScoped<ValuationTypeService>();
builder.Services.AddScoped<LoadValuationService>();
builder.Services.AddScoped<SellerConfigService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend", policy =>
//    {
//        //    policy.WithOrigins(
//        //        "http://localhost:5173",
//        //        "http://20.197.15.82:8090",
//        //        "http://20.197.15.82:8080",
//        //        "*" // <- Allow all origins for development
//        //        ) // <- React app URL on Azure
//        //          .AllowAnyMethod()
//        //          .AllowAnyHeader()
//        //          .AllowCredentials()
//        //          .AllowAnyOrigin();
//        //});
//        policy.WithOrigins(
//        "*" // <- Allow all origins for development
//        ) // <- React app URL on Azure
//          .AllowAnyMethod()
//          .AllowAnyHeader()
//          .AllowCredentials()
//          .AllowAnyOrigin();
//    });

//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()       // Allow all domains
            .AllowAnyHeader()       // Allow all headers
            .AllowAnyMethod();      // Allow all HTTP methods
    });
});
var app = builder.Build();
app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // Ensures OnModelCreating().HasData() is applied
}

app.UseMiddleware<RequestLoggingMiddleware>();
// Configure the HTTP request pipeline.
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction())
{

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CAMUNDA API POC");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
