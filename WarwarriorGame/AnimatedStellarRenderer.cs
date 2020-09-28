using SDL2;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace WarwarriorGame
{
    class AnimatedStellarRenderer : StellarRenderer
    {
        protected static new IntPtr texture;
        protected SDL.SDL_Rect srcRect;
        protected int frame = 0;
        const int TOTAL_FRAMES = 24;

        protected static new int width, height;

        public AnimatedStellarRenderer(StellarBase stellarObjec) : base(stellarObjec)
        {
        }

        public static new void LoadInit(IntPtr rendererPtr, string texturePath)
        {
            texture = SDL_image.IMG_LoadTexture(rendererPtr, texturePath);
            SDL.SDL_QueryTexture(texture, out _, out _, out width, out height);
        }

        public override void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.w = 128 * SCALE;
            dstRect.h = 128 * SCALE;

            srcRect.x = 0;
            srcRect.y = 0;
            srcRect.w = 128;
            srcRect.h = 128;

            center.x = srcRect.w / 2 * SCALE;
            center.y = srcRect.h / 2 * SCALE;

            dstRect.x = (int)(stellarObject.Position.X - Camera.Position.X);
            dstRect.y = (int)(stellarObject.Position.Y - Camera.Position.Y);

            srcRect.x = frame / 10 * srcRect.w;
            frame++;

            if (frame / 10 >= TOTAL_FRAMES)
                frame = 0;

            SDL.SDL_RenderCopyEx(rendererPtr, texture, ref srcRect, ref dstRect,
                0.0f, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public static new void Cleanup()
        {
            SDL.SDL_DestroyTexture(texture);
        }
    }
}
