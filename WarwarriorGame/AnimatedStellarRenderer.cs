using SDL2;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace WarwarriorGame
{
    class AnimatedStellarRenderer : StellarRenderer
    {
        protected SDL.SDL_Rect srcRect;
        protected int frame = 0;
        const int TOTAL_FRAMES = 24;

        public AnimatedStellarRenderer(StellarBase stellarObjec) : base(stellarObjec)
        {
        }

        public override void LoadInit(IntPtr rendererPtr, string texturePath)
        {
            base.LoadInit(rendererPtr, texturePath);

            dstRect.w = 128 * SCALE;
            dstRect.h = 128 * SCALE;

            srcRect.x = 0;
            srcRect.y = 0;
            srcRect.w = 128;
            srcRect.h = 128;

            center.x = srcRect.w / 2 * SCALE;
            center.y = srcRect.h / 2 * SCALE;
        }

        public override void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.x = (int)(stellarObject.Position.X - Camera.Position.X);
            dstRect.y = (int)(stellarObject.Position.Y - Camera.Position.Y);

            srcRect.x = frame / 10 * srcRect.w;
            frame++;

            if (frame / 10 >= TOTAL_FRAMES)
                frame = 0;

            SDL.SDL_RenderCopyEx(rendererPtr, texture, ref srcRect, ref dstRect,
                0.0f, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}
