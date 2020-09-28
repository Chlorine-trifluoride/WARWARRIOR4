using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class EnemyShipRenderer : ShipRenderer
    {
        private static IntPtr spriteSheet;
        private static int width, height;

        public EnemyShipRenderer(Actor actor) : base(actor)
        {

        }
        public static new void LoadInit(IntPtr rendererPtr, string spriteSheetPath)
        {
            spriteSheet = SDL_image.IMG_LoadTexture(rendererPtr, spriteSheetPath);
            Console.WriteLine(SDL.SDL_GetError());

            SDL.SDL_QueryTexture(spriteSheet, out _, out _, out int w, out int h);
            width = w / 12;
            height = h / 2;
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

            SDL.SDL_RenderCopyEx(rendererPtr, spriteSheet, ref srcRect, ref dstRect,
                actor.RotationDegrees, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public static new void Cleanup()
        {
            SDL.SDL_DestroyTexture(spriteSheet);
        }
    }
}
