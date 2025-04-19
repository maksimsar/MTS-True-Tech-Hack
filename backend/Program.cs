using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MTSTrueTechHack.Backend;
using MTSTrueTechHack.Data;

var builder = WebApplication.CreateBuilder(args);

// 1) Controllers
builder.Services.AddControllers();
// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Репозиторий
builder.Services.AddScoped<ISchemaRepository, SchemaRepository>();

// Сервис бизнес‑логики
builder.Services.AddScoped<ISchemaService, SchemaService>();


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
    var svc = scope.ServiceProvider.GetRequiredService<ISchemaService>();
    // передаём «заглушечный» ключ (GptClient можно настроить так, чтобы возвращал "{}")
    var testReq = new CreateSchemaRequest 
    { 
        UserId = 1, 
        Name = "Test", 
        Description = "desc" 
        // остальные поля, если есть 
    };
    var result = await svc.CreateAsync(testReq);
    Console.WriteLine("Generated JSON Schema: " + result.JsonSchema);
}


app.Run();