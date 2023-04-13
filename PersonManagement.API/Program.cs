using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using PizzApp.API.Infrastructure.Extensions;
using PizzApp.API.Infrastructure.Mappings;
using PizzApp.API.Middlewares;
using PizzApp.Persistence;
using PizzApp.Web.Infrastructure.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(option =>
//{
//    option.OperationFilter<SwaggerDefaultValues>();

//    //option.SwaggerDoc("v1", new OpenApiInfo
//    //{
//    //    Title = "Person Api",
//    //    Version = "v1",
//    //    Description = "Api to manage Persons",
//    //    Contact = new OpenApiContact
//    //    {
//    //        Email = "itacademy@tbcbank.com",
//    //        Name = "TBC IT Academy",
//    //        Url = new Uri("https://tbcacademy.ge")
//    //    }
//    //});

//    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var xmlPath = Path.Combine($"{AppContext.BaseDirectory}", xmlFile);

//    option.IncludeXmlComments(xmlPath);
//    //option.ExampleFilters();
//});

builder.Services.AddServices();



builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


builder.Services.RegisterMaps();

var app = builder.Build();
app.UseMiddleware<RequestResponseMiddleware>();
app.UseMiddleware<MyExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestCulture();

app.UseAuthorization();

app.MapControllers();

app.Run();
