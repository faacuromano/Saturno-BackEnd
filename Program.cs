using SATURNO_V2.Data;
using SATURNO_V2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

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
builder.Services.AddScoped<ListaServices>();

builder.Services.AddSwaggerGen(setupAction =>
         {
             setupAction.AddSecurityDefinition("Saturno_V2", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
             {
                 Type = SecuritySchemeType.Http,
                 Scheme = "Bearer",
                 Description = "Acá pegar el token generado al loguearse."
             });

             setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Saturno_V2"
                            } //Tiene que coincidir con el id seteado arriba en la definición

                        },  new List<string>()
                    }
             });
         });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Profesional", policy => policy.RequireClaim("TipoCuenta", "P"));
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
