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
            Vector2 temp = speed;
            Resistance();

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

            if (temp.X > 0 && speed.X < 0 || temp.X < 0 && speed.X > 0)
            {
                speed.X = 0;
            }

            if (temp.Y > 0 && speed.Y < 0 || temp.Y < 0 && speed.Y > 0)
            {
                speed.Y = 0;
            }

            if (position.X < 0 || position.X > 1200 - radius)
            {
                speed.X *= -1;
                if (position.X < 0)
                {
                    position.X = 1;
                }
                else
                {
                    position.X = 1199 - radius;
                }
            }

            if (position.Y < 0 || position.Y > 700 - radius)
            {
                speed.Y *= -1;

                if (position.Y < 0)
                {
                    position.Y = 1;
                }
                else
                {
                    position.Y = 699 - radius;
                }
            }

            if (float.IsNaN(position.X) || float.IsNaN(position.Y))
            {
                Console.WriteLine("Error occured. Position is NaN");
            }

            for (int j = 0; j < balls.Count; j++)
            {
                // Makes sure ball isn't checked against itself
                if (balls[i] != balls[j])
                {
                    if (balls[j].speed.X > Config.maxVel) { balls[j].speed.X = Config.maxVel; }
                    if (balls[j].speed.Y > Config.maxVel) { balls[j].speed.Y = Config.maxVel; }

                    if (balls[j].position.X < 0 || balls[j].position.X > 1200 - balls[j].radius)
                    {
                        balls[j].speed.X *= -1;

                        if (balls[j].position.X < 0)
                        {
                            balls[j].position.X = 1;
                        }
                        else
                        {
                            balls[j].position.X = 1199 - balls[j].radius;
                        }
                    }

                    if (balls[j].position.Y < 0 || balls[j].position.Y > 700 - balls[j].radius)
                    {
                        balls[j].speed.Y *= -1;

                        if (balls[j].position.Y < 0)
                        {
                            balls[j].position.Y = 1;
                        }
                        else
                        {
                            balls[j].position.Y = 699 - balls[j].radius;
                        }
                    }

                    if (Colliding(balls[j]) & Config.shouldCollide)
                    {
                        ResolveCollision(balls[j]);
                    }
                }
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
