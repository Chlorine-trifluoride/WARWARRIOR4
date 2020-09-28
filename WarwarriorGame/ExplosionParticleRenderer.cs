using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class ExplosionParticleRenderer : ParticleRenderer
    {
        protected new Particle particle;
        protected static new IntPtr texture;

        public ExplosionParticleRenderer(Particle particle) : base(particle)
        {
            this.particle = particle;
        }

        public static new void LoadInit(IntPtr rendererPtr, string texturePath)
        {
            texture = SDL_image.IMG_LoadTexture(rendererPtr, texturePath);

            SDL.SDL_QueryTexture(texture, out _, out _, out width, out height);

            center.x = width / 2 * scale;
            center.y = height / 2 * scale;
        }

        public new void Cleanup()
        {
            SDL.SDL_DestroyTexture(texture);
        }

        public override void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.w = width * scale;
            dstRect.h = height * scale;

            dstRect.x = (int)(particle.Position.X - Camera.Position.X);
            dstRect.y = (int)(particle.Position.Y - Camera.Position.Y);

            SDL.SDL_RenderCopyEx(rendererPtr, texture, IntPtr.Zero, ref dstRect,
                0.0f, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}
