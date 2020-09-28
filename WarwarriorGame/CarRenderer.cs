using System;
using SDL2;
using WarwarriorGame;

namespace WarwarriorGame
{
    class CarRenderer : IRenderer
    {
        private Car car;
        private IntPtr carTexture;
        private SDL.SDL_Rect dstRect;
        private SDL.SDL_Point center;

        private const int scale = 1;
        private int halfCarLen;

        public CarRenderer(Car car)
        {
            this.car = car;
        }
        
        public void LoadInit(IntPtr rendererPtr, string texturePath)
        {
            carTexture = SDL_image.IMG_LoadTexture(rendererPtr, texturePath);

            SDL.SDL_QueryTexture(carTexture, out _, out _, out int w, out int h);

            dstRect.w = w * scale;
            dstRect.h = h * scale;

            center.x = w / 2 * scale;
            center.y = h / 2 * scale;

            halfCarLen = w / 2 * scale;
        }

        public void Cleanup()
        {
            SDL.SDL_DestroyTexture(carTexture);
        }

        public void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.x = (int)(car.Position.X - Camera.Position.X);
            dstRect.y = (int)(car.Position.Y - Camera.Position.Y);

            SDL.SDL_RenderCopyEx(rendererPtr, carTexture, IntPtr.Zero, ref dstRect,
                car.RotationDegrees, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public Vector2 GetCenter()
        {
            throw new NotImplementedException();
        }
    }
}
