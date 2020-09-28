using System;

namespace WarwarriorGame
{
    class Car
    {
        public CarRenderer Renderer { get; set; }
        public bool IsEngineOn { get; private set; } = false;
        public float Acceleration { get; private set; } = 0.0f;
        public float Rotation { get; private set; } = 0.0f;
        public float Velocity { get; private set; } = 0.0f;
        public Vector2 Position { get; private set; }
        public float RotationDegrees => Utils.RadianToDegrees(Rotation);
        public int VelocityInKmh => (int)(Velocity * 0.2f);

        private float steerDirection = 0.0f;

        public Car(Vector2 position, float rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        public Car() : this(Vector2.Zero, 0.0f)
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

        private void Steer(float direction)
        {
            steerDirection += direction;
        }

        public void SteerLeft() => Steer(-1.0f);

        public void SteerRight() => Steer(1.0f);

        public void Update(float deltaTime)
        {
            Velocity *= 0.95f; // slow down from resistances

            Velocity += Acceleration * 8.0f;
            Acceleration = 0; // bit hacky

            if (Velocity < 0.0f) Velocity = 0.0f; // Don't accelerate backwards

            Rotation += steerDirection * MathF.Sqrt(Velocity) * deltaTime * 0.2f; // * 0.02f;
            steerDirection *= 0.5f;

            // Update position
            Vector2 directionVector = Utils.RadianToVector2(Rotation);
            Position += directionVector * Velocity * deltaTime;
        }
    }
}
