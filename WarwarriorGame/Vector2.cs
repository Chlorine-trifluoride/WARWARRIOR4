using System;
using System.Runtime.CompilerServices;

namespace WarwarriorGame
{
    struct Vector2
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
            => new Vector2(a.X + b.X, a.Y + b.Y);

        public static Vector2 operator *(Vector2 v, float f)
            => new Vector2(v.X * f, v.Y * f);

        public static Vector2 operator -(Vector2 a, Vector2 b)
             => new Vector2(a.X - b.X, a.Y - b.Y);

        public static Vector2 Zero => new Vector2(0.0f, 0.0f);

        public static Vector2 Random()
        {
            Random random = new Random();
            return new Vector2((float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f);
        }

        public override string ToString()
        {
            return $"[{X}]:[{Y}]";
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float f)
        {
            return b * f + a * (1.0f - f);
        }

        public static float Angle(Vector2 a, Vector2 b)
        {
            return MathF.Atan2(a.Y - b.Y, a.X - b.X);
        }

        public static float AngleTo(Vector2 a, Vector2 b)
        {
            return MathF.Atan2(Cross(a, b), Dot(a, b));
        }

        public static float Cross(Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            Vector2 difference = a - b;
            Vector2 direction = difference.Normalize();
            return Vector2.Dot(direction, difference);
        }
    }

    static class Vector2Extensions
    {
        public static Vector2 Normalize(this Vector2 a)
        {
            float distance = MathF.Sqrt(a.X * a.X + a.Y * a.Y);
            Vector2 v = new Vector2(a.X / distance, a.Y / distance);
            return v;
        }

        public static float Magnitude(this Vector2 a)
        {
            return MathF.Sqrt(a.X * a.X + a.Y * a.Y);
        }
    }
}