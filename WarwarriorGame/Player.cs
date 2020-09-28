using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WarwarriorGame
{
    class Player : Actor
    {
        public static Player Inst;

        public Player(Vector2 position, float rotation) : base(position, rotation) 
        {
            Inst = this;
            Renderer = new ShipRenderer(this);
        }
    }
}
