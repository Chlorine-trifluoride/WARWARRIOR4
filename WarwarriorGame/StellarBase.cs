using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class StellarBase
    {
        public static List<StellarBase> stellarObjects = new List<StellarBase>();
        public IRenderer Renderer { get; set; }
        public float Mass { get; set; }
        public Vector2 Position { get; set; }

        public StellarBase(Vector2 position, float mass)
        {
            stellarObjects.Add(this);
            this.Position = position;
            this.Mass = mass;

            Renderer = new StellarRenderer(this);
        }
    }
}
