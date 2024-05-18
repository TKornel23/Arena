using Arena.Domain;
using Arena.Persistence;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

[assembly: FunctionsStartup(typeof(Arena.AzureApi.Startup))]
namespace Arena.AzureApi;

public class Startup : FunctionsStartup
{

    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<IGenericRepository<Round>, GenericRepository<Round>>();
        builder.Services.AddScoped<IGenericRepository<Domain.Arena>, GenericRepository<Domain.Arena>>();
        builder.Services.AddDbContext<ArenaContext>(x => x.UseSqlite("Data Source=../../../../Arena.Persistence/Database/ArenaDatabase.db"));

        builder.Services.AddLogging();
    }
}