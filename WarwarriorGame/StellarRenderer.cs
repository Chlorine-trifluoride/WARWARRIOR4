using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class StellarRenderer
    {
        protected StellarBase stellarObject;
        protected static IntPtr texture;
        protected SDL.SDL_Rect dstRect;
        protected SDL.SDL_Point center;

        protected static int width, height;

        protected int SCALE = 5;

        public StellarRenderer(StellarBase stellarObject)
        {
            this.stellarObject = stellarObject;
            SCALE = (int)(stellarObject.Mass / 500);
        }

        public static void LoadInit(IntPtr rendererPtr, string texturePath)
        {
            texture = SDL_image.IMG_LoadTexture(rendererPtr, texturePath);
            SDL.SDL_QueryTexture(texture, out _, out _, out width, out height);
        }

        public static void Cleanup()
        {
            SDL.SDL_DestroyTexture(texture);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(center.x, center.y);
        }

        public virtual void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.w = width * SCALE;
            dstRect.h = height * SCALE;

            center.x = width / 2 * SCALE;
            center.y = height / 2 * SCALE;

            dstRect.x = (int)(stellarObject.Position.X - Camera.Position.X);
            dstRect.y = (int)(stellarObject.Position.Y - Camera.Position.Y);

            SDL.SDL_RenderCopyEx(rendererPtr, texture, IntPtr.Zero, ref dstRect,
                0.0f, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}
