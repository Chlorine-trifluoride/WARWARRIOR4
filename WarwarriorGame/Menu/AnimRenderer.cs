using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame.Menu
{
    class AnimRenderer
    {
        private IntPtr spriteSheet;
        private SDL.SDL_Rect srcRect;
        private SDL.SDL_Rect dstRect;
        private SDL.SDL_Point center;
        private int rows;
        private int columns;
        private int width, height;
        private int row = 0;
        private int column = 0;

        public void LoadInit(IntPtr rendererPtr, string spriteSheetPath, int rows, int columns, float scale)
        {
            spriteSheet = SDL_image.IMG_LoadTexture(rendererPtr, spriteSheetPath);
            SDL.SDL_QueryTexture(spriteSheet, out _, out _, out int totalW, out int totalH);

            this.rows = rows;
            this.columns = columns;

            width = totalW / rows;
            height = totalH / columns;

            dstRect.w = (int)(width * scale);
            dstRect.h = (int)(height * scale);

            srcRect.x = 0;
            srcRect.y = 0;
            srcRect.w = width;
            srcRect.h = height;

            center.x = (int)(srcRect.w / 2 * scale);
            center.y = (int)(srcRect.h / 2 * scale);
        }

        private void SetSrcRect()
        {
            column++;
            
            if (column >= columns)
            {
                column = 0;
                row++;
            }

            if (row >= rows)
                row = 0;

            srcRect.x = column * width;
            srcRect.y = row * height;
        }

        public void Render(IntPtr rendererPtr, GameBase game, Vector2 position)
        {
            SetSrcRect();

            dstRect.x = (int)(position.X);
            dstRect.y = (int)(position.Y);

            SDL.SDL_RenderCopyEx(rendererPtr, spriteSheet, ref srcRect, ref dstRect,
                0.0f, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public void Cleanup()
        {
            SDL.SDL_DestroyTexture(spriteSheet);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(center.x, center.y);
        }
    }
}
