
using SolRural.Data;
using SolRural.Service;

namespace SolRural
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<CultivoDbSettings>
                (builder.Configuration.GetSection("SolRuralDatabase"));

            builder.Services.AddSingleton<CultivoService>();

            builder.Services.Configure<FazendaDbSettings>
                (builder.Configuration.GetSection("SolRuralDatabase"));

            builder.Services.AddSingleton<FazendaService>();

            builder.Services.Configure<InstalacaoDbSettings>
                (builder.Configuration.GetSection("SolRuralDatabase"));

            builder.Services.AddSingleton<InstalacaoService>();

            builder.Services.Configure<LocalizacaoDbSettings>
                (builder.Configuration.GetSection("SolRuralDatabase"));

            builder.Services.AddSingleton<LocalizacaoService>();

            builder.Services.Configure<MedicaoEnergDbSettings>
               (builder.Configuration.GetSection("SolRuralDatabase"));

            builder.Services.AddSingleton<MedicaoEnergService>();

            builder.Services.Configure<ProprietarioDbSettings>
               (builder.Configuration.GetSection("SolRuralDatabase"));

            builder.Services.AddSingleton<ProprietarioService>();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Sol Rural API",
                    Description = "API para Administração de Fazendas Parceiras",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Sol Rural",
                        Email = "solrural@gmail.com",
                    }
                });
            });


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
