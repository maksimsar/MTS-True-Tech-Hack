using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation; 
using Microsoft.EntityFrameworkCore;
using MTSTrueTechHack.Backend;
using MTSTrueTechHack.Backend.Services;
using MTSTrueTechHack.Backend.Validators;
using MTSTrueTechHack.Data;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// DI
builder.Services.AddScoped<ISchemaRepository, SchemaRepository>();
builder.Services.AddScoped<ISchemaService, SchemaService>();
builder.Services.AddHttpClient<GptClient>();

// Validation
builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<CreateSchemaRequestValidator>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core + PostgreSQL
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Сидируем БД
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(db);
}

// Проверяем AutoMapper
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
app.UseAuthorization();
app.MapControllers();
app.Run();
