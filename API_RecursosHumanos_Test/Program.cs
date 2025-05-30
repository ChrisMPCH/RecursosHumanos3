using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Controllers;
using RecursosHumanosCore.Data;
using RecursosHumanosCore.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión
PostgreSQLDataAccess.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurar CORS

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar servicios
builder.Services.AddScoped<ContratoController>();
builder.Services.AddScoped<UsuariosController>();
builder.Services.AddScoped<EmpleadosDataAccess>();
builder.Services.AddScoped<AsistenciaDataAccess>();
builder.Services.AddScoped<AusenciaController>();
builder.Services.AddScoped<PostgreSQLDataAccess>();

// Configurar health check
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configurar el pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
