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

// Configura la cadena de conexi�n
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registra los repositorios en el contenedor de servicios
builder.Services.AddScoped<EventRepository>(sp => new EventRepository(connectionString));
builder.Services.AddScoped<Metodos>(); // Registrar la clase Metodos

// Configuraci�n para JWT
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

// Configuraci�n para Swagger (Documentaci�n de API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // T�tulo y versi�n de Swagger
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Software Lion", Version = "v1" });

    // Configuraci�n del esquema de seguridad para JWT
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

// Configuraci�n de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configura el middleware para archivos est�ticos
app.UseStaticFiles(); // Permite servir archivos est�ticos desde wwwroot

app.UseHttpsRedirection();
app.UseAuthentication(); // Middleware de autenticaci�n
app.UseAuthorization();  // Middleware de autorizaci�n

// Configura la p�gina de inicio
app.MapGet("/", () => Results.File("wwwroot/index.html"));

app.MapControllers();

app.Run();

