using AirportSerever.BL;
using AirportSerever.Data;
using AirportSerever.Hubs;
using AirportSerever.Models;
using AirportSerever.Repository;
using AirportSerever.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AirportContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AirportConnection")));
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
//builder.Services.AddScoped<Service>();
//builder.Services.AddScoped<IStation, StationRepository>();
//builder.Services.AddScoped<IRepository<Graph>, GraphRepository>();
builder.Services.AddSingleton<Runway>();
//builder.Services.AddSingleton<AirportHub>();
builder.Services.AddSingleton<ControlTower>();

var app = builder.Build();

app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<AirportHub>("/airport");


app.Run();



