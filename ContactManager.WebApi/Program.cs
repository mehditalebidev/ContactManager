
using ContactManager.Application;
using ContactManager.Infrastructure;
using ContactManager.WebApi;
using ContactManager.WebApi.Common.Behaviors;
using ContactManager.WebApi.Common.Errors;
using ContactManager.WebApi.Common.Mapping;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    
    builder.Services.AddHttpContextAccessor();



    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));


    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddPresentation();
    
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();

    app.MapControllers();
    app.Run();
}

