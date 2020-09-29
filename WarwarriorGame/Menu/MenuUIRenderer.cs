using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame.Menu
{
    class MenuUIRenderer
    {
        private IntPtr fontPtr;
        private SDL.SDL_Color textColor;
        private SDL.SDL_Color activeTextColor;
        private IntPtr surface;
        private IntPtr texture;
        private SDL.SDL_Rect dstRect;

        public MenuUIRenderer()
        {
        }

        public void LoadInit(IntPtr rendererPtr)
        {
            fontPtr = SDL_ttf.TTF_OpenFont("media/font/typed.ttf", 25);

            if (fontPtr == IntPtr.Zero)
                Console.WriteLine("Error loading font. SDL: {0}", SDL.SDL_GetError());

            textColor = new SDL.SDL_Color
            {
                r = 217,
                g = 204,
                b = 0,
                a = 255
            };

            activeTextColor = new SDL.SDL_Color
            {
                r = 255,
                g = 125,
                b = 0,
                a = 255
            };
        }

        public void Cleanup()
        {
            SDL_ttf.TTF_CloseFont(fontPtr);
        }

        public void Render(IntPtr rendererPtr, GameBase game, string text, Vector2 position, bool isActive)
        {
            surface = SDL_ttf.TTF_RenderText_Solid(fontPtr, text, (isActive) ? activeTextColor : textColor);
            texture = SDL.SDL_CreateTextureFromSurface(rendererPtr, surface);

            SDL.SDL_QueryTexture(texture, out _, out _, out int w, out int h);
            dstRect.x = (int)position.X;
            dstRect.y = (int)position.Y;
            dstRect.w = w;
            dstRect.h = h;

            SDL.SDL_RenderCopy(rendererPtr, texture, IntPtr.Zero, ref dstRect);

            SDL.SDL_DestroyTexture(texture);
            SDL.SDL_FreeSurface(surface);
        }
    }
}
