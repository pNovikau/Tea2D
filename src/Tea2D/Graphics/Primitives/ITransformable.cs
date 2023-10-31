using Tea2D.Common;

namespace Tea2D.Graphics.Primitives;

public interface ITransformable : IDestroyable
{
    Vector2<float> Position { get; set; }
    Vector2<float> Scale { get; set; }
    Vector2<float> Origin { get; set; }

    float Rotation { get; set; }
}