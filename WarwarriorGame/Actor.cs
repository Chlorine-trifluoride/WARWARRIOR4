using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    abstract class Actor
    {
        public IRenderer Renderer { get; set; }
        public bool IsEngineOn { get; private set; } = true;
        public float Acceleration { get; private set; } = 0.0f;
        public float Rotation { get; private set; } = 0.0f;
        public float Velocity { get; private set; } = 0.0f;
        public Vector2 Position { get; private set; }
        public float RotationDegrees => Utils.RadianToDegrees(Rotation);
        public Vector2 Heading => Utils.RadianToVector2(Rotation);
        public int VelocityInKmh => (int)(Velocity * 0.2f);

        private float steerDirection = 0.0f;

        public Actor(Vector2 position, float rotation)
        {
            this.Position = position;
            this.Rotation = rotation;

            Renderer = new ActorRenderer(this);
        }

        public Actor() : this(Vector2.Zero, 0.0f)
        {
        }

        public bool TurnEngineOn()
        {
            if (IsEngineOn)
                return false;

            return IsEngineOn = true;
        }

        public bool TurnEngineOff()
        {
            if (!IsEngineOn)
                return false;

            IsEngineOn = false;
            return true;
        }

        public bool Accelerate(float amount = 1.0f)
        {
            if (!IsEngineOn)
                return false;

            Acceleration = amount;
            return true;
        }

        public void Decelerate(float amount = 1.0f)
        {
            Acceleration = -amount;
        }

        protected void Steer(float direction)
        {
            steerDirection += direction;
        }

        public void SteerLeft() => Steer(-1.0f);

        public void SteerRight() => Steer(1.0f);

        public int GetSteerDirection()
        {
            if (steerDirection < -0.1f)
                return -1;
            else if (steerDirection > 0.1f)
                return 1;

            return 0;
        }

        public virtual void Update(float deltaTime)
        {
            Velocity *= 0.98f; // slow down from resistances

            Velocity += Acceleration * 5.0f;
            Acceleration = 0; // bit hacky

            if (Velocity < 0.0f) Velocity = 0.0f; // Don't accelerate backwards

            Rotation += steerDirection * MathF.Sqrt(Velocity) * deltaTime * 0.1f; // * 0.02f;
            steerDirection *= 0.5f;

            // Update position
            Vector2 directionVector = Utils.RadianToVector2(Rotation);
            Position += directionVector * Velocity * deltaTime;
        }
    }
}
