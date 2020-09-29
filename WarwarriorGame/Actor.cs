using System;
using System.Collections.Generic;
using System.Text;

namespace WarwarriorGame
{
    abstract class Actor
    {
        public static List<Actor> Actors { get; } = new List<Actor>();
        public ActorRenderer Renderer { get; set; }
        public Shield Shield { get; set; }
        public bool IsEngineOn { get; private set; } = true;
        public float Acceleration { get; private set; } = 0.0f;
        public float Rotation { get; private set; } = 0.0f;
        public float Velocity { get; private set; } = 0.0f;
        public Vector2 Position { get; private set; }
        public float RotationDegrees => Utils.RadianToDegrees(Rotation);
        public Vector2 Heading => Utils.RadianToVector2(Rotation);
        public int VelocityInKmh => (int)(Velocity * 0.2f);
        public Vector2 Origin => Position + Renderer.GetCenter();
        public virtual float Mass { get; set; } = 20.0f;

        private float steerDirection = 0.0f;

        public Actor(Vector2 position, float rotation)
        {
            this.Position = position;
            this.Rotation = rotation;

            Renderer = new ActorRenderer(this);
            Shield = new Shield(this);
            Actors.Add(this);
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

        public virtual bool DoesCollideWith(Particle particle)
        {
            if (Utils.GetDistance(particle.Position, Origin) < Shield.Radius * 32.0f)
                return true;

            return false;
        }

        public virtual void AddShieldHealth(int amount)
        {
            if (Shield.Remaining < 150)
                Shield.Remaining += amount;
        }

        public virtual void Fire()
        {
            new Projectile(Position + Renderer.GetCenter(), Heading * (Velocity * 0.001f + 1.0f), Rotation);
        }

        public virtual void Update(float deltaTime)
        {
            Velocity *= 0.99f; // slow down from resistances

            Velocity += Acceleration * 5.0f;
            Acceleration = 0; // bit hacky

            if (Velocity < 0.0f) Velocity = 0.0f; // Don't accelerate backwards

            Rotation += steerDirection * MathF.Sqrt(Velocity) * deltaTime * 0.1f; // * 0.02f;
            steerDirection *= 0.5f;

            // Update position
            Vector2 directionVector = Utils.RadianToVector2(Rotation);
            Position += directionVector * Velocity * deltaTime;
            ApplyGravity(deltaTime);
        }

        public virtual void ApplyGravity(float deltaTime)
        {
            Vector2 gravityVector = Vector2.Zero;

            for (int i = 0; i < StellarBase.stellarObjects.Count; i++)
            {
                Vector2 difference = StellarBase.stellarObjects[i].Position + StellarBase.stellarObjects[i].Renderer.GetCenter() - Position;
                Vector2 direction = difference.Normalize();

                // distance
                float distance = Vector2.Dot(direction, difference);
                gravityVector += Steer(direction, StellarBase.stellarObjects[i].Mass / (distance * distance));
            }

            gravityVector *= 4000.0f;
            Position += gravityVector * deltaTime;
        }

        private Vector2 Steer(Vector2 target, float amount)
        {
            return target * amount;
        }
    }
}
