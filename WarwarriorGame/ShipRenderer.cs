using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class ShipRenderer : ActorRenderer
    {
        private IntPtr spriteSheet;
        protected SDL.SDL_Rect dstRect;
        protected SDL.SDL_Rect srcRect;
        protected SDL.SDL_Point center;

        protected const int SCALE = 1;
        protected const int SPRITE_PIXELS = 64;

        public ShipRenderer(Actor actor) : base(actor)
        {
        }

        public override void LoadInit(IntPtr rendererPtr, string spriteSheetPath)
        {
            spriteSheet = SDL_image.IMG_LoadTexture(rendererPtr, spriteSheetPath);
            Console.WriteLine(SDL.SDL_GetError());

            SDL.SDL_QueryTexture(spriteSheet, out _, out _, out int w, out int h);
            w /= 12;
            h /= 2;

            dstRect.w = w * SCALE;
            dstRect.h = h * SCALE;

            center.x = w / 2 * SCALE;
            center.y = h / 2 * SCALE;

            srcRect.x = 0;
            srcRect.y = 0;
            srcRect.w = SPRITE_PIXELS;
            srcRect.h = SPRITE_PIXELS;
        }

        public override void Cleanup()
        {
            SDL.SDL_DestroyTexture(spriteSheet);
        }

        protected void SetSrcRect()
        {
            int steerDir = actor.GetSteerDirection();

            switch (steerDir)
            {
                case -1:
                    srcRect.x = SPRITE_PIXELS;
                    srcRect.y = 0;
                    break;

                case 1:
                    srcRect.x = SPRITE_PIXELS * 10;
                    srcRect.y = 0;
                    break;

                default:
                    srcRect.x = 0;
                    srcRect.y = 0;
                    break;
            }
        }

        public override void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.x = (int)(actor.Position.X - Camera.Position.X);
            dstRect.y = (int)(actor.Position.Y - Camera.Position.Y);

            SetSrcRect();

            SDL.SDL_RenderCopyEx(rendererPtr, spriteSheet, ref srcRect, ref dstRect,
                actor.RotationDegrees, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public override Vector2 GetCenter()
        {
            return new Vector2(center.x, center.y);
        }
    }
}
