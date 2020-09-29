using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WarwarriorGame
{
    class Player : Actor
    {
        public static Player Inst;
        public override float Mass { get; set; } = 50.0f;
        public int Score { get; set; } = 0;

        public Player(Vector2 position, float rotation) : base(position, rotation) 
        {
            Inst = this;
            Renderer = new PlayerShipRenderer(this);
        }

        // TODO: Hacky fixy
        public static Player GetRespawnNewPlayer()
        {
            return new Player(new Vector2(1008.0f, 808.0f), MathF.PI / 2);
        }
    }
}
