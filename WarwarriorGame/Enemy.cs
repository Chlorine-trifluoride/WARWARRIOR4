using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class Enemy : Actor
    {
        public static List<Enemy> Enemies = new List<Enemy>();
        protected uint nextFire = 0;
        protected const uint TIME_BETWEEN_FIRE = 1000;
        protected Random random = new Random();

        public Enemy(Vector2 position, float rotation) : base(position, rotation)
        {
            Renderer = new EnemyShipRenderer(this);
        }

        public virtual void FireIfShouldFire()
        {
            uint time = SDL.SDL_GetTicks();

            if (time > nextFire)
            {
                Fire();
                nextFire = (uint)(time + TIME_BETWEEN_FIRE + random.Next(0, 1000));
            }
        }

        public override void Update(float deltaTime)
        {
            Vector2 difference = Player.Inst.Position - Position;

            if ((difference.X * Heading.Y) > (difference.Y * Heading.X))
                Steer(-0.2f);
            else
                Steer(0.2f);

            Accelerate(0.4f);

            FireIfShouldFire();

            base.Update(deltaTime);
        }
    }
}
