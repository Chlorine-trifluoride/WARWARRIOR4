using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class StellarRenderer : IRenderer
    {
        protected StellarBase stellarObject;
        protected IntPtr texture;
        protected SDL.SDL_Rect dstRect;
        protected SDL.SDL_Point center;

        protected int SCALE = 5;

        public StellarRenderer(StellarBase stellarObject)
        {
            this.stellarObject = stellarObject;
            SCALE = (int)(stellarObject.Mass / 500);
        }

        public virtual void LoadInit(IntPtr rendererPtr, string texturePath)
        {
            texture = SDL_image.IMG_LoadTexture(rendererPtr, texturePath);

            SDL.SDL_QueryTexture(texture, out _, out _, out int w, out int h);

            dstRect.w = w * SCALE;
            dstRect.h = h * SCALE;

            center.x = w / 2 * SCALE;
            center.y = h / 2 * SCALE;
        }

        public void Cleanup()
        {
            SDL.SDL_DestroyTexture(texture);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(center.x, center.y);
        }

        public virtual void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.x = (int)(stellarObject.Position.X - Camera.Position.X);
            dstRect.y = (int)(stellarObject.Position.Y - Camera.Position.Y);

            SDL.SDL_RenderCopyEx(rendererPtr, texture, IntPtr.Zero, ref dstRect,
                0.0f, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}
