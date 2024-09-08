using CashFlow.Api.Converter;
using CashFlow.Api.Filters;
using CashFlow.Api.Middlewares;
using CashFlow.Application;
using CashFlow.Infrastructure;
using CashFlow.Infrastructure.Data.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config =>
{
    config.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = @"JWT Authorization header using the Bearer Token.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer <token>'",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey,
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddMvc(options => 
{
    options.Filters.Add(typeof(ExceptionFilter));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CashFlowWeb", policy =>
    {
        policy.WithOrigins("http://localhost:5109/", "https://localhost:7218");
    });
});

builder.Services.AddLocalization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Jwt:SigningKey"] ??
            throw new ArgumentNullException("Provide a Jwt signing key")))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures(new[] { "en", "pt-BR", "pt-PT", "fr" })
    .AddSupportedUICultures(new[] { "en", "pt-BR", "pt-PT", "fr" })
);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await MigrateDatabase();

app.Run();

async Task MigrateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();

    await DatabaseMigration.MigrateDatabase(scope.ServiceProvider);
}