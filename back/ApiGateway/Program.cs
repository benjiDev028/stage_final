using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IO;
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);

// Chemins vers vos fichiers de configuration
string[] ocelotConfigFiles = new string[] {
    "ocelot_auth.json",
    "ocelot_commentaire.json"
};

JObject ocelotConfig = new JObject();

foreach (var configFile in ocelotConfigFiles)
{
    // Fusionner le contenu de chaque fichier de configuration dans ocelotConfig
    var configJson = JObject.Parse(File.ReadAllText(Path.Combine(builder.Environment.ContentRootPath, configFile)));
    ocelotConfig.Merge(configJson, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });
}

// Sauvegarder la configuration fusionnée dans un fichier temporaire
var tempConfigFile = Path.GetTempFileName();
File.WriteAllText(tempConfigFile, ocelotConfig.ToString());

// Utiliser le fichier de configuration temporaire pour Ocelot
builder.Configuration.AddJsonFile(tempConfigFile);

builder.Services.AddOcelot();
builder.Services.AddCors();

var app = builder.Build();

app.MapControllers();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000")
           .AllowAnyHeader()
           .AllowAnyMethod();
});

await app.UseOcelot();
app.Run();

// Nettoyer le fichier temporaire après l'arrêt de l'application
File.Delete(tempConfigFile);
