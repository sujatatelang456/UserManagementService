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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // <- React app URL on Azure
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
var app = builder.Build();

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
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
