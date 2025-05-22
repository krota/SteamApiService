using SteamApiService.Services;
using SteamApiService.Settings;
using SteamApiService.Utils.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SteamSettings>(builder.Configuration.GetSection("Steam"));
builder.Services.AddHttpClient<ISteamService, SteamService>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new SnakeCaseToCamelCaseResolver();
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
