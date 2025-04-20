using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MTSTrueTechHack.Backend;
using MTSTrueTechHack.Data;
using MTSTrueTechHack.Backend.Models.Dtos;
using MTSTrueTechHack.Backend.Services;
using System.Diagnostics;
using MTSTrueTechHack.Backend.Validators;

var builder = WebApplication.CreateBuilder(args);

// 1) Controllers
builder.Services.AddControllers();
// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Репозиторий
builder.Services.AddScoped<ISchemaRepository, SchemaRepository>();

// Сервис бизнес‑логики
builder.Services.AddScoped<ISchemaService, SchemaService>();

builder.Services.AddHttpClient<GptClient>();


// 2) FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateSchemaRequestValidator>();


// 3) Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4) EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
// Checking
app.Run();