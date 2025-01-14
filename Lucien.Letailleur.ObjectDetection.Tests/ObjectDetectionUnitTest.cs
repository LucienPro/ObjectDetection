using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Lucien.Letailleur.ObjectDetection.Tests;

public class ObjectDetectionUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        var imageScenesData = new List<byte[]>();

        // Charger des images fictives depuis le dossier "Scenes"
        foreach (var imagePath in Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }

        var detectObjectInScenesResults = await new ObjectDetection().DetectObjectInScenesAsync(imageScenesData);

        // Vérifier les résultats pour la première image
        var expectedFirstBox = "[{\"Label\":\"person\",\"Confidence\":0.7419791,\"Dimensions\":{\"X\":0,\"Y\":0,\"Width\":2,\"Height\":2}}]";
        Assert.Equal(expectedFirstBox, JsonSerializer.Serialize(detectObjectInScenesResults[0].Box));

        // Vérifier les résultats pour la deuxième image
        var expectedSecondBox = "[{\"Label\":\"chair\",\"Confidence\":0.5,\"Dimensions\":{\"X\":0,\"Y\":0,\"Width\":2,\"Height\":2}}]";
        Assert.Equal(expectedSecondBox, JsonSerializer.Serialize(detectObjectInScenesResults[1].Box));
    }

    private static string GetExecutingPath()
    {
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }

}