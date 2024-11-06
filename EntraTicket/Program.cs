using EntraTicket.Data;
using EntraTicket.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EntraTicket;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor.
builder.Services.AddControllers();

// Configura la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registra los repositorios en el contenedor de servicios
builder.Services.AddScoped<EventRepository>(sp => new EventRepository(connectionString));
builder.Services.AddScoped<Metodos>(); // Registrar la clase Metodos

// Configuración para JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Configuración para Swagger (Documentación de API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Título y versión de Swagger
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Software Lion", Version = "v1" });

    // Configuración del esquema de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Requerimiento de seguridad para todos los endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configuración de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configura el middleware para archivos estáticos
app.UseStaticFiles(); // Permite servir archivos estáticos desde wwwroot

app.UseHttpsRedirection();
app.UseAuthentication(); // Middleware de autenticación
app.UseAuthorization();  // Middleware de autorización

// Configura la página de inicio
app.MapGet("/", () => Results.File("wwwroot/index.html"));

app.MapControllers();

app.Run();

