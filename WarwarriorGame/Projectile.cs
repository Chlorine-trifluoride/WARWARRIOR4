using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class Projectile : Particle
    {
        public float Rotation { get; set; }
        public float RotationDegrees => Utils.RadianToDegrees(Rotation);

        public Projectile(Vector2 position, Vector2 direction, float rotation) : base(position, direction)
        {
            this.Rotation = rotation;
            Renderer = new ProjectileRenderer(this);
        }

        public override void UpdateLogic(float deltaTime)
        {
            //for (int i = 0; i < StellarBase.stellarObjects.Count; i++)
            //{
            //    Vector2 difference = StellarBase.stellarObjects[i].Position + StellarBase.stellarObjects[i].Renderer.GetCenter() - Position;
            //    Vector2 direction = difference.Normalize();

            //    // distance
            //    float distance = Vector2.Dot(direction, difference);

            //    if ((difference.X * Heading.Y) > (difference.Y * Heading.X))
            //        Rotation -= deltaTime * MathF.Sqrt(Heading.Magnitude()) * 0.8f;
            //    else
            //        Rotation += deltaTime * MathF.Sqrt(Heading.Magnitude()) * 0.8f;
            //}

            base.UpdateLogic(deltaTime);
        }
    }
}
