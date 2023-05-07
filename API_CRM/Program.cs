using API_CRM.Class;
using API_CRM.Context;
using Microsoft.OpenApi.Models;

namespace API_CRM
{
    public class program
    {
        public static void Main(String[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API CRM Paye ton Kawa.",
                    Contact = new OpenApiContact
                    {
                        Name = "PayeTonKawa",
                        Email = "PayeTonKawa@gmail.com"
                    }
                });
                string xmlPath = Path.Combine(AppContext.BaseDirectory, "Swagger.xml");
                c.IncludeXmlComments(xmlPath);
            });

            //builder.Services.AddSingleton<ICRMApiService>(new CRMApiService()); //Context API 
            builder.Services.AddSingleton<ICRMApiService>(new CRMContextMock()); //Context Mock

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json","API CRM");
                    c.RoutePrefix = string.Empty;
                });
            }
            app.UseCors(builder => builder
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}