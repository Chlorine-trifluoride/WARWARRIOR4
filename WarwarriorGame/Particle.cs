using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class Particle
    {
        public ParticleRenderer Renderer;
        public Vector2 Position { get; set; }
        public Vector2 Heading { get; set; }

        public Particle(Vector2 position, Vector2 direction)
        {
            this.Position = position;
            this.Heading = direction;

            Renderer = new ParticleRenderer(this);
        }

        protected virtual void Steer(Vector2 target, float amount)
        {
            Heading += target * amount;
        }

        public virtual void UpdateLogic(float deltaTime)
        {
            for (int i = 0; i < StellarBase.stellarObjects.Count; i++)
            {
                Vector2 difference = StellarBase.stellarObjects[i].Position + StellarBase.stellarObjects[i].Renderer.GetCenter() - Position;
                Vector2 direction = difference.Normalize();

                // distance
                float distance = Vector2.Dot(direction, difference);

                Steer(direction, StellarBase.stellarObjects[i].Mass / (distance * distance));
            }

            Position += Heading * deltaTime * 500.0f;
        }
    }
}
