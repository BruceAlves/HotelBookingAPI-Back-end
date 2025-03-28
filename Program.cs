using HotelBookingAPI.Application;
using HotelBookingAPI.Application.Services;
using HotelBookingAPI.Domain;
using HotelBookingAPI.Infrastructure;
using HotelBookingAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using HotelBookingAPI.Application.Mappings; // Adicione esta linha

var builder = WebApplication.CreateBuilder(args);

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.SetIsOriginAllowed(origin => true) // Permite qualquer origem
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Se necessário para autenticação
        });
});

// Configurar conexão com SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar repositórios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtService>();

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(AuthMappingProfile)); // Substitua pelo seu perfil de mapeamento real

// Adicionar suporte a Controllers
builder.Services.AddControllers();

// 🚀 Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar autenticação JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// 🚀 Ativar Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin"); // Aplicar a política de CORS antes de mapear controllers

app.UseAuthentication();
app.UseAuthorization();

// 🚀 Adicionar mapeamento de Controllers (para evitar erro)
app.MapControllers();

app.Run();
