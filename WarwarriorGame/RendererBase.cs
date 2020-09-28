using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    abstract class RendererBase
    {
        public abstract void Cleanup();
        public abstract void LoadInit(IntPtr rendererPtr, string texturePath);
        public abstract void Render(IntPtr rendererPtr, GameBase game);
        public abstract Vector2 GetCenter();
    }
}
