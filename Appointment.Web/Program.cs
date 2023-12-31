using System.Linq;
using Appointment.Application;
using Appointment.Domain;
using Appointment.Infrastructure;
using Appointment.Persistence;
using Appointment.Presentation;
using Appointment.Presentation.Common.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Appointment.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services.AddPresentation();
            
                builder.Services.AddPersistence(builder.Configuration);
            
                builder.Services.AddInfrastructure(builder.Configuration);
            
                builder.Services.AddApplication();
                
                builder.Services.AddDomain();
            
                builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
            
                builder.Services.AddSwaggerGen();
            
                builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
            
                builder.Services.AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
                
                builder.Services.Configure<RouteOptions>(options =>
                {
                    options.ConstraintMap.Add("string", typeof(StringRouteConstraint));
                });
            }
            
            var app = builder.Build();
            {
                if (app.Environment.IsDevelopment())
                {
                    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        foreach (var groupName in apiVersionDescriptionProvider.ApiVersionDescriptions.Select(description =>
                                     description.GroupName))
                        {
                            options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
                        }
                    });
                }
            
                app.UseExceptionHandler("/error");
            
                app.UseHttpsRedirection();
            
                app.UseCors(CorsPolicies.LocalhostCorsPolicy);
            
                app.UseSerilogRequestLogging();
            
                app.UseAuthorization();
            
                app.UseStaticFiles();
            
                app.MapControllers();
            }
            
            app.Run();
        }
    }
}
