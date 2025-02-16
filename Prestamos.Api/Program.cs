using Pos.Infraestructure.ServiceCollections.ServiceCollections;
using Pos.Infraestructure.StarupConfigurations.ServiceCollections;
using Prestamos.Infraestructure.Mapeo;
using Prestamos.Infraestructure.Starup.ApplicationBuilder;

var builder = WebApplication.CreateBuilder(args);

/* Mapeo de entidades y los Objetos de Transferencia de Datos (DTO) */
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(AutoMapperEntidades).Assembly);

/* Controladores y extensiones */
ControllersService.AddControllersExtend(builder.Services, builder.Configuration);

/* Contexto de Base de Datos*/
DbContextService.AddDbContexts(builder.Services, builder.Configuration);

/* Contenedor de Inversión de Control (IoC)  */
IoC.AddDependency(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
DefaultAppConfig.InitConfigApi(app, builder.Environment);

app.Run();
