using BusinessObject;
using CarRentingAPI;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.FileProviders;
using Microsoft.OData.Edm;
using Repositories;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped(typeof(CarRentalDbContext));
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddControllers().AddOData(options => options.Select().Filter().Expand().OrderBy().Count().SetMaxTop(null));

// Add services to the container.
builder.Services.AddControllers().AddOData(
    o => o.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null)
    .AddRouteComponents("odata", EDMModelBuilder.GetEDMModel())
        );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRentingAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
