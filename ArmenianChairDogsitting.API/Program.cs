using ArmenianChairDogsitting.API;
using ArmenianChairDogsitting.API.CustomExceptionMiddleware;
using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data;
using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Dog Sitting", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorization: Bearer JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            Array.Empty<string>()
        },
    });
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = AuthOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddDbContext<ArmenianChairDogsittingContext>(o =>
{
    o.UseSqlServer("Server=80.78.240.16;Database=ArmenianChairDogsitting.DB;User Id=student;Password=qwe!23;");
});

builder.Services.AddScoped<ISitterRepository, SitterRepository>();

builder.Services.AddScoped<IClientsRepository, ClientsRepository>();

builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<ICommentsService, CommentsService>();

builder.Services.AddAutoMapper(typeof(APIMapperConfigStorage));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
