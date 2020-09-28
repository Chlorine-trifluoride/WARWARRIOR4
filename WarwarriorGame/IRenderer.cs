using System;

namespace WarwarriorGame
{
    interface IRenderer
    {
        void Cleanup();
        void LoadInit(IntPtr rendererPtr, string texturePath);
        void Render(IntPtr rendererPtr, GameBase game);
        Vector2 GetCenter();
    }
}