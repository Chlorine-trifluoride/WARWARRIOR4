using SDL2;
using System;

namespace WarwarriorGame
{
    class UI
    {
        private IntPtr fontPtr;
        private SDL.SDL_Color textColor;
        private IntPtr surface;
        private IntPtr texture;
        private SDL.SDL_Rect dstRect;
        private string text;

        public UI()
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
        }

        public void Cleanup()
        {
            SDL_ttf.TTF_CloseFont(fontPtr);
        }


        public void Update(float deltaTime)
        {
            // keep fixed spacing
            string rotation = $"Rotation: {Player.Inst.RotationDegrees}".PadRight(13, ' ');
            text = $"{rotation} Speed: {Player.Inst.VelocityInKmh} Particles: {Particle.Particles.Count} FPS: {(int)(1.0f/deltaTime)} Score: {Player.Inst.Score}";
        }

        public void Render(IntPtr rendererPtr, GameBase game)
        {
            surface = SDL_ttf.TTF_RenderText_Solid(fontPtr, text, textColor);
            texture = SDL.SDL_CreateTextureFromSurface(rendererPtr, surface);

            SDL.SDL_QueryTexture(texture, out _, out _, out int w, out int h);
            dstRect.x = 0;
            dstRect.y = 0;
            dstRect.w = w;
            dstRect.h = h;

            SDL.SDL_RenderCopy(rendererPtr, texture, IntPtr.Zero, ref dstRect);

            SDL.SDL_DestroyTexture(texture);
            SDL.SDL_FreeSurface(surface);
        }
    }
}
