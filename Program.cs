
using SATURNO_V2.Data;
using SATURNO_V2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<SaturnoV2Context>(builder.Configuration.GetConnectionString("Connection"));


builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProfesionalService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<TurnoService>();
builder.Services.AddScoped<ServicioService>();

var app = builder.Build();

app.UseCors(builder =>
    builder
    .WithOrigins("http://localhost:3000", "https://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
