using WarwarriorGame;
using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class Background
    {
        private IntPtr backgroundTexture;
        private SDL.SDL_Rect dstRect;
        private SDL.SDL_Point center;

        private const int SCALE = 3;
        private const float CAMERA_SCALE = 0.3f;

        public void LoadInit(IntPtr rendererPtr, string texturePath)
        {
            backgroundTexture = SDL_image.IMG_LoadTexture(rendererPtr, texturePath);

            if (backgroundTexture == IntPtr.Zero)
                Console.WriteLine("Error loading background texture. {0}", SDL.SDL_GetError());

            SDL.SDL_QueryTexture(backgroundTexture, out _, out _, out int w, out int h);

            dstRect.w = w * SCALE;
            dstRect.h = h * SCALE;

            center.x = w / 2 * SCALE;
            center.y = h / 2 * SCALE;
        }

        public void Cleanup()
        {
            SDL.SDL_DestroyTexture(backgroundTexture);
        }

        public void Render(IntPtr rendererPtr, GameBase game)
        {
            // TODO: proper looping

            for (int x = -4; x < 4; x++)
            {
                for (int y = -4; y < 4; y++)
                {
                    dstRect.x = (int)(-Camera.Position.X * CAMERA_SCALE) + dstRect.w * x;
                    dstRect.y = (int)(-Camera.Position.Y * CAMERA_SCALE) + dstRect.h * y;

                    SDL.SDL_RenderCopyEx(rendererPtr, backgroundTexture, IntPtr.Zero, ref dstRect,
                        0.0f, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
                }
            }
        }
    }
}
