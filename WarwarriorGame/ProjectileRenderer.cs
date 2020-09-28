using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class ProjectileRenderer : ParticleRenderer
    {
        protected Projectile projectile;

        public ProjectileRenderer(Projectile projectile) : base(projectile)
        {
            this.projectile = projectile;
        }

        public override void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.w = width * scale;
            dstRect.h = height * scale;

            dstRect.x = (int)(particle.Position.X - Camera.Position.X);
            dstRect.y = (int)(particle.Position.Y - Camera.Position.Y);

            SDL.SDL_RenderCopyEx(rendererPtr, texture, IntPtr.Zero, ref dstRect,
                projectile.RotationDegrees, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}
