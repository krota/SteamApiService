using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SteamApiService.Services;
using SteamApiService.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SteamSettings>(builder.Configuration.GetSection("Steam"));
builder.Services.AddHttpClient<ISteamService, SteamService>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();
