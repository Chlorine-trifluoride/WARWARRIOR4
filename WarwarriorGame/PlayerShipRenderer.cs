using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class PlayerShipRenderer : ActorRenderer
    {
        private static IntPtr SpriteSheet;
        protected SDL.SDL_Rect dstRect;
        protected SDL.SDL_Rect srcRect;
        protected SDL.SDL_Point center;

        protected const int SCALE = 1;
        protected const int SPRITE_PIXELS = 64;

        protected static int width, height;

        public PlayerShipRenderer(Actor actor) : base(actor)
        {
        }

        public static new void LoadInit(IntPtr rendererPtr, string spriteSheetPath)
        {
            SpriteSheet = SDL_image.IMG_LoadTexture(rendererPtr, spriteSheetPath);
            Console.WriteLine(SDL.SDL_GetError());

            SDL.SDL_QueryTexture(SpriteSheet, out _, out _, out int w, out int h);
            width = w/12;
            height = h/2;


        }

        public override void Cleanup()
        {
            SDL.SDL_DestroyTexture(SpriteSheet);
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
            dstRect.w = width * SCALE;
            dstRect.h = height * SCALE;

            center.x = width / 2 * SCALE;
            center.y = height / 2 * SCALE;

            srcRect.x = 0;
            srcRect.y = 0;
            srcRect.w = SPRITE_PIXELS;
            srcRect.h = SPRITE_PIXELS;

            dstRect.x = (int)(actor.Position.X - Camera.Position.X);
            dstRect.y = (int)(actor.Position.Y - Camera.Position.Y);

            SetSrcRect();

            SDL.SDL_RenderCopyEx(rendererPtr, SpriteSheet, ref srcRect, ref dstRect,
                actor.RotationDegrees, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public override Vector2 GetCenter()
        {
            return new Vector2(center.x, center.y);
        }
    }
}
