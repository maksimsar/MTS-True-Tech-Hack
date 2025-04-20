// Program.cs
using AutoMapper;
using FluentValidation.AspNetCore;         // .AddFluentValidationAutoValidation()
using FluentValidation;                    // для валидаторов
using Microsoft.EntityFrameworkCore;       // UseNpgsql, MigrateAsync
using MTSTrueTechHack.Backend;             // MappingProfile, DbSeeder
using MTSTrueTechHack.Backend.Services;    // ISchemaRepository, SchemaRepository, ISchemaService, SchemaService, GptClient
using MTSTrueTechHack.Backend.Validators;  // CreateSchemaRequestValidator
using MTSTrueTechHack.Data;                // AppDbContext

var builder = WebApplication.CreateBuilder(args);

// 1) CORS (React на 5173/3000)
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.WithOrigins("http://localhost:5173", "http://localhost:3000")
     .AllowAnyHeader()
     .AllowAnyMethod()));

// 2) Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3) AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 4) FluentValidation
builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<CreateSchemaRequestValidator>();

// 5) EF Core + PostgreSQL
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 6) DI-сервисы
builder.Services.AddScoped<ISchemaRepository, SchemaRepository>();
builder.Services.AddScoped<ISchemaService, SchemaService>();
builder.Services.AddHttpClient<GptClient>();

var app = builder.Build();

// 7) Сидим БД перед маппингом контроллеров
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(db);
}

// 8) Проверка конфигурации AutoMapper
app.Services
   .GetRequiredService<IMapper>()
   .ConfigurationProvider
   .AssertConfigurationIsValid();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
