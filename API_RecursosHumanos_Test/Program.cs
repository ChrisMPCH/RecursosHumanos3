using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Controllers;
using RecursosHumanosCore.Data;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar la conexión a la base de datos
PostgreSQLDataAccess.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar los controladores
builder.Services.AddSingleton<UsuariosController>();
builder.Services.AddSingleton<EmpleadosController>();
builder.Services.AddSingleton<ContratoController>();
builder.Services.AddSingleton<AsistenciaController>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
