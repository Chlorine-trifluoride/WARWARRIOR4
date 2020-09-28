using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class AnimatedStellarObject : StellarBase
    {
        public AnimatedStellarObject(Vector2 position, float mass) : base(position, mass) 
        {
            Renderer = new AnimatedStellarRenderer(this);
        }
    }
}
