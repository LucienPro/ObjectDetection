using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using Lucien.Letailleur.ObjectDetection;
using Microsoft.AspNetCore.Mvc;
    
var builder = WebApplication.CreateBuilder(args);
    
// Ajouter Swagger pour tester l'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Activer Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Route HTTP POST pour détecter les objets
app.MapPost("/ObjectDetection", async ([FromForm] IFormFileCollection files) =>
{
    if (files.Count < 1)
        return Results.BadRequest("Aucun fichier n'a été fourni.");

    // Lire le fichier envoyé en tant que tableau de bytes
    using var sceneSourceStream = files[0].OpenReadStream();
    using var sceneMemoryStream = new MemoryStream();
    await sceneSourceStream.CopyToAsync(sceneMemoryStream);
    var imageSceneData = sceneMemoryStream.ToArray();

    try
    {
        // Utiliser votre librairie ObjectDetection
        var objectDetection = new Lucien.Letailleur.ObjectDetection.ObjectDetection();
        var results = await objectDetection.DetectObjectInScenesAsync(new List<byte[]> { imageSceneData });

        // Obtenir l'image modifiée avec les zones détectées
        var imageData = results[0].ImageData;

        // Retourner l'image avec les zones détectées
        return Results.File(imageData, "image/jpeg");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Une erreur s'est produite : {ex.Message}");
    }
}).DisableAntiforgery();

app.Run();

