using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class ProjectileRenderer : ParticleRenderer
    {
        protected Projectile projectile;
        protected bool firedByPlayer;
        protected static IntPtr friendlyTexture;
        protected static IntPtr enemyTexture;

        public ProjectileRenderer(Projectile projectile, bool firedByPlayer) : base(projectile)
        {
            this.projectile = projectile;
            this.firedByPlayer = firedByPlayer;
        }
        public static void LoadInit(IntPtr rendererPtr, string friendlyFireTexturePath, string enemyFireTexturePath)
        {
            friendlyTexture = SDL_image.IMG_LoadTexture(rendererPtr, friendlyFireTexturePath);
            enemyTexture = SDL_image.IMG_LoadTexture(rendererPtr, enemyFireTexturePath);
            SDL.SDL_QueryTexture(texture, out _, out _, out width, out height);

            center.x = width / 2 * scale;
            center.y = height / 2 * scale;
        }

        public override void Render(IntPtr rendererPtr, GameBase game)
        {
            dstRect.w = width * scale;
            dstRect.h = height * scale;

            dstRect.x = (int)(particle.Position.X - Camera.Position.X);
            dstRect.y = (int)(particle.Position.Y - Camera.Position.Y);

            SDL.SDL_RenderCopyEx(rendererPtr, (firedByPlayer) ? friendlyTexture : enemyTexture, IntPtr.Zero, ref dstRect,
                projectile.RotationDegrees, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}
