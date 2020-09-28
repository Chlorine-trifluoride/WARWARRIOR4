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
            }

            base.UpdateLogic(deltaTime);
        }
    }
}
