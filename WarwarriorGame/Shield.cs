using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class Shield
    {
        //public static List<Shield> shields { get; set; }
        private Actor actor;
        public ShieldRenderer Renderer { get; set; }
        public int Remaining { get; set; } = 100;
        public float Radius => Remaining / 75.0f + 0.2f;

        public Shield(Actor actor)
        {
            this.actor = actor;
            //shields.Add(this);
            Renderer = new ShieldRenderer(actor);
        }

        public void OnHit(Vector2 fromHeading)
        {
            Remaining -= 10;

            if (Remaining <= 0)
                Explode();

            else
                SpawnEnergyParticles(fromHeading);
        }

        public void SpawnEnergyParticles(Vector2 fromHeading)
        {
            for (int i = 0; i < 5; i++)
            {
                new ShieldParticle(actor.Origin + fromHeading.Normalize() * Radius * 64.0f, fromHeading * 0.05f + Vector2.Random() * 0.8f);
            }
        }

        private void Explode()
        {
            for (int i = 0; i < 40; i++)
            {
                Vector2 direction = Vector2.Random().Normalize();
                new ExplosionParticle(actor.Position + direction * 64.0f, direction * 0.5f);
            }

            Actor.Actors.Remove(actor);
        }
    }
}
