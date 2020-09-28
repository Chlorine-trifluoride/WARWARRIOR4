using System;
using SDL2;
using WarwarriorGame;

namespace WarwarriorGame
{
    class ActorRenderer
    {
        protected Actor actor;
        private IntPtr texture;
        private SDL.SDL_Rect dstRect;
        private SDL.SDL_Point center;

        private const int scale = 1;

        public ActorRenderer(Actor actor)
        {
            this.actor = actor;
        }

        public virtual void LoadInit(IntPtr rendererPtr, string texturePath)
        {
            texture = SDL_image.IMG_LoadTexture(rendererPtr, texturePath);

            SDL.SDL_QueryTexture(texture, out _, out _, out int w, out int h);

            dstRect.w = w * scale;
            dstRect.h = h * scale;

            center.x = w / 2 * scale;
            center.y = h / 2 * scale;
        }

        public virtual void Cleanup()
        {
            SDL.SDL_DestroyTexture(texture);
        }

        public virtual void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.x = (int)(actor.Position.X - Camera.Position.X);
            dstRect.y = (int)(actor.Position.Y - Camera.Position.Y);

            SDL.SDL_RenderCopyEx(rendererPtr, texture, IntPtr.Zero, ref dstRect,
                actor.RotationDegrees, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public virtual Vector2 GetCenter()
        {
            return new Vector2(center.x, center.y);
        }
    }
}
