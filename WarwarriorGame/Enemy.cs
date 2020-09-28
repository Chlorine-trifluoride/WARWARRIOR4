using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class Enemy : Actor
    {
        public Enemy(Vector2 position, float rotation) : base(position, rotation)
        {
            Renderer = new ShipRenderer(this);
        }

        public override void Update(float deltaTime)
        {
            Vector2 difference = Player.Inst.Position - Position;

            if ((difference.X * Heading.Y) > (difference.Y * Heading.X))
                Steer(-0.2f);
            else
                Steer(0.2f);

            Accelerate(0.2f);


            base.Update(deltaTime);
        }
    }
}
