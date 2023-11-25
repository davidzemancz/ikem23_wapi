
using ikem23_wapi.Services;
using System.Net.Http;

namespace ikem23_wapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ImportTemplateDataService>();
            builder.Services.AddScoped<PatientRecordDataService>();
            builder.Services.AddScoped<PatientDataService>();
            builder.Services.AddScoped<ExcelReaderService>();
            builder.Services.AddScoped<HttpClient>((sp) =>
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("x-api-key", Globals.FHIRServerApiKey);
                return httpClient;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
