using WarwarriorGame;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    static class Camera
    {
        public static Vector2 Position { get; set; } = Vector2.Zero;

        public static void Update(Vector2 followTarget, GameBase gameBase, float deltaTime)
        {
            Vector2 targetPosition = new Vector2(followTarget.X - gameBase.WindowWidth / 2.0f, followTarget.Y - gameBase.WindowHeight / 2.0f);
            Position = Vector2.Lerp(Position, targetPosition, 0.8f * deltaTime);
        }
    }
}
