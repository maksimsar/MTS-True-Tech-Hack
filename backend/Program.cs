using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MTSTrueTechHack.Backend;
using MTSTrueTechHack.Data;
using MTSTrueTechHack.Backend.Models.Dtos;
using MTSTrueTechHack.Backend.Services;
using System.Diagnostics;

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
builder.Services.AddFluentValidationClientsideAdapters();
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

using (var scope = app.Services.CreateScope())
{
    var gpt = scope.ServiceProvider.GetRequiredService<GptClient>();

    // Создаём DTO (четвёртый параметр — исходная JSON-схема, для теста оставляем пустой)
    var createDto = new CreateSchemaDto(
        UserId: 1,
        Name: "Test",
        Description: "desc",
        JsonSchema: ""
    );
    var stopwatch = Stopwatch.StartNew();
var schemaJson = await gpt.GenerateSchemaAsync(
    new CreateSchemaDto(1, "TestObject", "Пример описания", "")
);
stopwatch.Stop();

Console.WriteLine("GPT JSON Schema:\n" + schemaJson);
Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
    // Вызываем заглушку
    var result = await gpt.GenerateSchemaAsync(createDto);

    Console.WriteLine("GPT returned: " + result);
}



app.Run();