using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class ParticleRenderer
    {
        protected Particle particle;
        protected static IntPtr texture;
        protected SDL.SDL_Rect dstRect;
        protected static  SDL.SDL_Point center;
        protected static int width, height;

        protected const int scale = 1;

        public ParticleRenderer(Particle particle)
        {
            this.particle = particle;
        }

        public static void LoadInit(IntPtr rendererPtr, string texturePath)
        {
            texture = SDL_image.IMG_LoadTexture(rendererPtr, texturePath);

            SDL.SDL_QueryTexture(texture, out _, out _, out width, out height);

            center.x = width / 2 * scale;
            center.y = height / 2 * scale;
        }

        public void Cleanup()
        {
            SDL.SDL_DestroyTexture(texture);
        }

        public virtual void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.w = width * scale;
            dstRect.h = height * scale;

            dstRect.x = (int)(particle.Position.X - Camera.Position.X);
            dstRect.y = (int)(particle.Position.Y - Camera.Position.Y);

            SDL.SDL_RenderCopyEx(rendererPtr, texture, IntPtr.Zero, ref dstRect,
                0.0f, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public Vector2 GetCenter()
        {
            throw new NotImplementedException();
        }
    }
}
