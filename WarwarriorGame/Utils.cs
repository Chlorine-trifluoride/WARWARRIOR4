using System;

namespace WarwarriorGame
{
    class Utils
    {
        public static Vector2 RadianToVector2(float rad)
        {
            return new Vector2(
                MathF.Sin(rad), // x
                -MathF.Cos(rad)); // y
        }

        public static int RadianToDegrees(float rad)
        {
            int degrees = (int)(rad * 180 / MathF.PI) % 360;

            if (degrees < 0)
                degrees += 360;

            return degrees;
        }

        public static float AngleFromVector2(Vector2 v)
        {
            if (v.X < 0)
            {
                return 360 - (MathF.Atan2(v.X, -v.Y) * 360 / (MathF.PI * 2) * -1);
            }
            else
            {
                return MathF.Atan2(v.X, -v.Y) * 360 / (MathF.PI * 2);
            }
        }

        public static float GetDistance(Vector2 vect1, Vector2 vect2)
        {
            float x = vect1.X - vect2.X;
            float y = vect1.Y - vect2.Y;

            return MathF.Sqrt(x * x + y * y);
        }

        public static float GetDirection(Vector2 vect1, Vector2 vect2)
        {
            float pxRes = vect1.X - vect2.X;
            float pyRes = vect1.Y - vect2.Y;


            if (pxRes > 0.0f)
                return (float)(Math.Atan(pyRes / pxRes) + Math.PI);
            else
                return (float)(Math.Atan(pyRes / pxRes) + (2 * Math.PI));
        }
    }
}