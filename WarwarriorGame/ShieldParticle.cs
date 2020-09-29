using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class ShieldParticle : Particle
    {
        public ShieldParticle(Vector2 position, Vector2 direction) : base(position, direction)
        {
            Renderer = new ShieldParticleRenderer(this);
        }

        public override void UpdateLogic(float deltaTime)
        {
            for (int i = 0; i < Actor.Actors.Count; i++)
            {
                if (Actor.Actors[i].DoesCollideWith(this))
                {
                    Actor.Actors[i].AddShieldHealth(2);
                    Particles.Remove(this);
                }

                Vector2 difference = Actor.Actors[i].Origin - Position;
                Vector2 direction = difference.Normalize();

                // distance
                float distance = Vector2.Dot(direction, difference);

                Steer(direction, Actor.Actors[i].Mass / (distance * distance));
            }

            base.UpdateLogic(deltaTime);
        }
    }
}
