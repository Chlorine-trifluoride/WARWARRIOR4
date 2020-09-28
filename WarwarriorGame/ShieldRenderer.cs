using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class ShieldRenderer
    {
        private Actor actor;
        private static IntPtr texture;
        private SDL.SDL_Rect dstRect;
        private SDL.SDL_Point center;
        private static int width, height;

        private const int scale = 1;

        public ShieldRenderer(Actor actor)
        {
            this.actor = actor;
        }

        public static void LoadInit(IntPtr rendererPtr, string texturePath)
        {
            texture = SDL_image.IMG_LoadTexture(rendererPtr, texturePath);

            SDL.SDL_QueryTexture(texture, out _, out _, out width, out height);
        }

        public void Cleanup()
        {
            SDL.SDL_DestroyTexture(texture);
        }

        public void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.w = (int)(width * scale * actor.Shield.Radius);
            dstRect.h = (int)(height * scale * actor.Shield.Radius);

            center.x = (int)(dstRect.w);
            center.y = (int)(dstRect.h);

            dstRect.x = (int)(actor.Origin.X - center.x / 2 - Camera.Position.X);
            dstRect.y = (int)(actor.Origin.Y - center.y / 2 - Camera.Position.Y);


            SDL.SDL_RenderCopyEx(rendererPtr, texture, IntPtr.Zero, ref dstRect,
                0.0f, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(center.x, center.y);
        }
    }
}
