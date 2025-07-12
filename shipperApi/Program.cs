using Microsoft.EntityFrameworkCore;
using shipperApi.Models.DataBase;
using Microsoft.AspNetCore.Mvc;
using shipperApi.Endpoints;
using shipperApi.Services;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ShippingDevContex>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddXmlSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
   
    //options.SchemaFilter<XmlSchemaFilter>();
    options.CustomSchemaIds(type => type.FullName);
});
builder.Services.AddScoped<IReportGeneratorService, FastReportGeneratorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapShippingSessionLoginEndpoint();
app.MapCreateExpeditionEndpoint();


app.Run();

