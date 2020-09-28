using SDL2;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WarwarriorGame
{
    class Projectile : Particle
    {
        public static List<Projectile> projectiles = new List<Projectile>();
        private uint spawnTime;
        public float Rotation { get; set; }
        public float RotationDegrees => Utils.RadianToDegrees(Rotation);

        private const uint TIME_TO_LIVE = 500;

        public Projectile(Vector2 position, Vector2 direction, float rotation) : base(position, direction)
        {
            spawnTime = SDL.SDL_GetTicks();
            projectiles.Add(this);
            this.Rotation = rotation;
            Renderer = new ProjectileRenderer(this);
        }

        public override void UpdateLogic(float deltaTime)
        {
            if (SDL.SDL_GetTicks() - spawnTime > TIME_TO_LIVE)
            {
                for (int i = 0; i < Actor.Actors.Count; i++)
                {
                    Vector2 difference = Actor.Actors[i].Position + Actor.Actors[i].Renderer.GetCenter() - Position;
                    Vector2 direction = difference.Normalize();

                    // distance
                    float distance = Vector2.Dot(direction, difference);

                    if (distance < Actor.Actors[i].Shield.Radius * 64 + 16)
                    {
                        Actor.Actors[i].Shield.OnHit(Heading);
                        Particles.Remove(this);
                    }
                }
            }

            base.UpdateLogic(deltaTime);
        }
    }
}
