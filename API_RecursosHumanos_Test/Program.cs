using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Controllers;
using RecursosHumanosCore.Data;
using RecursosHumanosCore.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Configura aqu� tus cadenas de conexi�n si es necesario
// Por ejemplo:
// PosgresSQLAccess.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra aqu� todos los controladores y servicios que inyectas en RecursosHumanosControllerAPI_test
builder.Services.AddScoped<ContratoController>();
builder.Services.AddScoped<UsuariosController>();
builder.Services.AddScoped<EmpleadosDataAccess>();
builder.Services.AddScoped<AsistenciaDataAccess>();
builder.Services.AddScoped<AusenciaController>();

// Si usas PostgreSQLDataAccess como singleton o scoped, registra tambi�n:
builder.Services.AddScoped<PostgreSQLDataAccess>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
