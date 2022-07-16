namespace Tea2D.Common.Math;

public static class VectorMath
{
    public static float Dot(in Vector2F value1, in Vector2F value2)
    {
        return value1.X * value2.X 
             + value1.Y * value2.Y;
    }

    public static Vector2F Normalize(Vector2F value)
    {
        var dot = Dot(value, value);
        var length = System.MathF.Sqrt(dot);

        if (length != 0)
            value /= length;

        return value;
    }

    public static Vector2F GetDirection(Vector2F startPoint, Vector2F endPoint)
    {
        var normal = endPoint - startPoint;
        var angle = System.Math.Atan2(normal.Y, normal.X);

        return new Vector2F(System.MathF.Cos((float)angle), System.MathF.Sin((float)angle));
    }
}