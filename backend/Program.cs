using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MTSTrueTechHack.Backend;
using MTSTrueTechHack.Backend.Services;
using MTSTrueTechHack.Data;
using MTSTrueTechHack.Backend.Validators;
using AutoMapper;
using FluentValidation; // для IMapper

var builder = WebApplication.CreateBuilder(args);

// 1) Controllers
builder.Services.AddControllers();

// 2) AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 3) Репозиторий и сервисы
builder.Services.AddScoped<ISchemaRepository, SchemaRepository>();
builder.Services.AddScoped<ISchemaService,     SchemaService>();

// 4) HTTP‑клиент для GPT
builder.Services.AddHttpClient<GptClient>();

// 5) FluentValidation
builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<CreateSchemaRequestValidator>();

// 6) Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 7) EF Core + PostgreSQL
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// *НЕ* обязательно, но можно проверить маппинги сразу:
var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();