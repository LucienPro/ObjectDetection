using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectDetection;

namespace Lucien.Letailleur.ObjectDetection;

public class ObjectDetection
{
    public async Task<IList<ObjectDetectionResult>> DetectObjectInScenesAsync(IList<byte[]> imagesSceneData)
    {
        await Task.Delay(1000); // Simuler un délai pour imiter un traitement réel

        return new List<ObjectDetectionResult>
        {
            new ObjectDetectionResult
            {
                ImageData = new byte[0], // Données d'image fictives
                Box = new List<BoundingBox>
                {
                    new BoundingBox
                    {
                        Label = "person",
                        Confidence = 0.7419791f,
                        Dimensions = new BoundingBoxDimensions
                        {
                            X = 0, Y = 0, Width = 2, Height = 2
                        }
                    }
                }
            },
            new ObjectDetectionResult
            {
                ImageData = new byte[0], // Données d'image fictives
                Box = new List<BoundingBox>
                {
                    new BoundingBox
                    {
                        Label = "chair",
                        Confidence = 0.5f,
                        Dimensions = new BoundingBoxDimensions
                        {
                            X = 0, Y = 0, Width = 2, Height = 2
                        }
                    }
                }
            }
        };
    }

}