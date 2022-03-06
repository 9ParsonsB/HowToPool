using System;
using System.Numerics;
using System.Collections.Generic;

namespace HowToPool
{
    class Entity
    {
        public Vector2 position;
        public Vector2 speed;
        public Vector2 origin;
        public float angle;
        public float mass;
        public float radius;

        public void Update(List<Entity> balls, int i, float dt)
        {
            Resistance();

            Vector2 temp = speed;
            if (Config.shouldResist)
            {
                if (speed.X != 0)
                {
                    if (speed.X < 0) { speed.X += Config.resistance; }
                    if (speed.X > 0 && temp.X == speed.X) { speed.X -= Config.resistance; }
                }

                if (speed.Y != 0)
                {
                    if (speed.Y < 0) { speed.Y += Config.resistance; }
                    if (speed.Y > 0 && temp.Y == speed.Y) { speed.Y -= Config.resistance; }
                }
            }

            if (float.IsNaN(position.X) || float.IsNaN(position.Y))
            {
                Console.WriteLine("Error occured. Position is NaN");
            }
        }

        public bool Colliding(Entity ball)
        {
            float xd = position.X - ball.position.X;
            float yd = position.Y - ball.position.Y;

            float sumRadius = radius + ball.radius;
            float sqrRadius = sumRadius * sumRadius;

            float distSqr = (xd * xd) + (yd * yd);

            if (distSqr <= sqrRadius)
            {
                return true;
            }
            return false;
        }

        public void Resistance()
        {
            Vector2 temp = speed;

            if (Config.shouldResist)
            {
                if (speed.X != 0)
                {
                    if (speed.X < 0) { speed.X += mass * Config.resistance; }

                    if (speed.X > 0 && temp.X == speed.X)
                    {
                        speed.X -= mass * Config.resistance;
                    }
                }

                if (speed.Y != 0)
                {
                    if (speed.Y < 0) { speed.Y += mass / Config.resistance; }
                    if (speed.Y > 0 && temp.Y == speed.Y) { speed.Y -= mass * Config.resistance; }
                }
            }

            if (temp.X > 0 && speed.X < 0 || temp.X < 0 && speed.X > 0)
            {
                speed.X = 0;
            }

            if (temp.Y > 0 && speed.Y < 0 || temp.Y < 0 && speed.Y > 0)
            {
                speed.Y = 0;
            }
        }

        public void ResolveCollision(Entity other)
        {
            // Get the minimum translation distance
            // Used to push bals apart after intersecting
            Vector2 delta = (position - other.position);
            float d = delta.Length();
            Vector2 mtd = delta * (((radius + other.radius) - d) / d);

            // Resolve intersection
            // inverse mass quantities
            float im1 = 1 / mass;
            float im2 = 1 / other.mass;

            // Push-pull them apart based off their mass
            speed = speed + (mtd * (im1 / (im1 + im2)));
            other.speed = other.speed - (mtd * (im2 / (im1 + im2)));

            speed.X = MathF.Round(speed.X, 4);
            speed.Y = MathF.Round(speed.Y, 4);

            if (float.IsNaN(position.X) || float.IsNaN(position.Y))
            {
                Console.WriteLine("Error occured. Position is NaN");
            }

            other.speed.X = MathF.Round(other.speed.X, 4);
            other.speed.Y = MathF.Round(other.speed.Y, 4);

            // Impact speed
            Vector2 v = (speed - (other.speed));
            v = Vector2.Normalize(v);

            float vn = Vector2.Dot(v, v);

            // Sphere intersecting but moving away from each other already
            if (vn > 0.0f)
                return;

            // Collision impulse
            float i = (-(1.0f + Config.resistance) * vn) / (im1 + im2);
            Vector2 impulse = mtd * i;

            // Change in momentum
            speed = speed + (impulse * im1);
            other.speed = other.speed - (impulse * im2);
        }
    }
}
