using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    class StellarBase
    {
        public static List<StellarBase> stellarObjects = new List<StellarBase>();
        public StellarRenderer Renderer { get; set; }
        public float Mass { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin => Position + Renderer.GetCenter();
        public float Radius => Renderer.GetRadius();

        public StellarBase(Vector2 position, float mass)
        {
            stellarObjects.Add(this);
            this.Position = position;
            this.Mass = mass;

            Renderer = new StellarRenderer(this);
        }
    }
}
