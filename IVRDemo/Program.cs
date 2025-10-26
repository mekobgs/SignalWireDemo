using IVRDemo.Interfaces;
using IVRDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services for DI
builder.Services.AddScoped<IVoiceFlowService, VoiceFlowService>();
builder.Services.AddScoped<IXmlResponseBuilder, XmlResponseBuilder>();
builder.Services.AddScoped<IMenuPromptService, MenuPromptService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
