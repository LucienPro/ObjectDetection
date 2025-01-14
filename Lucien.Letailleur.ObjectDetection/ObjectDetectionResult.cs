namespace Lucien.Letailleur.ObjectDetection;

public record ObjectDetectionResult
{
    public byte[] ImageData { get; set; }
    public IList<BoundingBox> Box { get; set; }
};

public class BoundingBox
{
    public string Label { get; set; } // Le label de l'objet détecté
    public float Confidence { get; set; } // La confiance associée à la détection
    public BoundingBoxDimensions Dimensions { get; set; } // Les dimensions du cadre englobant
}

public class BoundingBoxDimensions
{
    public float X { get; set; } // Coordonnée X du cadre englobant
    public float Y { get; set; } // Coordonnée Y du cadre englobant
    public float Width { get; set; } // Largeur du cadre englobant
    public float Height { get; set; } // Hauteur du cadre englobant
}
