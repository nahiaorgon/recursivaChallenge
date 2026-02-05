using Horoscopo.Core.Business;
using Horoscopo.Core.Business.Interfaces;
using Horoscopo.Core.Configuration;
using Horoscopo.Core.Configuration.Interfaces;
using Horoscopo.Core.Repository;
using Horoscopo.Core.Repository.Interfaces;
using Horoscopo.Services;
using Horoscopo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirBlazor", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var horoscopoConfig = HoroscopoConfig.Build(builder.Configuration);
builder.Services.AddSingleton<IHoroscopoConfig>(horoscopoConfig);

builder.Services.AddScoped<ISignoRepository, SignoRepository>();
builder.Services.AddScoped<ISignoBusiness, SignoBusiness>();
builder.Services.AddScoped<ISignoServices, SignoServices>();

builder.Services.AddHttpClient<SignoServices>(client =>
{
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientHoroscopo");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();  
}

app.UseCors("PermitirBlazor");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
