
using DataProcessor.API.Extensions;
using DataProcessor.API.Filters;

namespace DataProcessor.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<CommonExceptionFilter>();
            });

            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
                loggingBuilder.AddFile("logs/snow-api-{Date}.txt");
            });
            builder.Services.ConfigureServices();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", builder =>
                {
                    builder.WithOrigins("http://localhost:6006")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAngular");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
