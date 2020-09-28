using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class ExplosionParticle : Particle
    {
        public ExplosionParticle(Vector2 position, Vector2 direction) : base(position, direction)
        {
            Renderer = new ExplosionParticleRenderer(this);
        }

        public override void UpdateLogic(float deltaTime)
        {
            for (int i = 0; i < Actor.Actors.Count; i++)
            {
                if (Actor.Actors[i].DoesCollideWith(this))
                {
                    Vector2 direction = (Position - Actor.Actors[i].Position).Normalize();
                    Heading += direction;
                }
            }

            base.UpdateLogic(deltaTime);
        }
    }
}
