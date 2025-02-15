using FluentValidation;
using Serilog;
using Serilog.Exceptions;
using StudioTG.Application.DTO.Requests;
using StudioTG.Infrastructure;
using StudioTG.Infrastructure.Common;
using StudioTG.Web.Validators;
using StudioTG.Web.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.WithExceptionDetails()
    .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.Configure<FieldOptions>(builder.Configuration.GetRequiredSection(FieldOptions.SectionName));

builder.Services.AddInfrastructureServices();

builder.Services.AddScoped<IValidator<NewGameRequest>, NewGameValidator>();
builder.Services.AddScoped<IValidator<MakeTurnRequest>, MakeTurnValidator>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.AllowAnyOrigin();
        });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.EnableTryItOutByDefault());
}

app.UseCors();

app.UseAuthentication();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

Log.CloseAndFlush();