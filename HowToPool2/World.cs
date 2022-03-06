using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace HowToPool
{
    /// <summary>
    /// Stores simulation state, config and systems.
    /// </summary>
    class World
    {
        public List<Entity> entities = new List<Entity>();
        public Cue cue = new Cue();
        public Random rng = new Random();
        int minV = 1000;
        int maxV = 1800;

        public void Load()
        {
            rng = new Random((int)DateTime.UtcNow.Ticks);

            AddRandomBalls();

            // Need to be first(White ball)
            Entity cueBall = AddBall(new Vector2(300, Config.height / 2), new Vector2(300, 0));

            AddBall(new Vector2(Config.width - (Config.width / 3), Config.height / 2), Vector2.Zero);

            AddBall(new Vector2(Config.width - (Config.width / 3) + 29, (Config.height / 2) - 14), Vector2.Zero);
            AddBall(new Vector2(Config.width - (Config.width / 3) + 29, (Config.height / 2) + 14), Vector2.Zero);

            AddBall(new Vector2(Config.width - (Config.width / 3) + 57, (Config.height / 2)), Vector2.Zero);
            AddBall(new Vector2(Config.width - (Config.width / 3) + 57, (Config.height / 2) - 27), Vector2.Zero);
            AddBall(new Vector2(Config.width - (Config.width / 3) + 57, (Config.height / 2) + 27), Vector2.Zero);

            AddBall(new Vector2(Config.width - (Config.width / 3) + 85, (Config.height / 2) - 14), Vector2.Zero);
            AddBall(new Vector2(Config.width - (Config.width / 3) + 85, (Config.height / 2) - 41), Vector2.Zero);
            AddBall(new Vector2(Config.width - (Config.width / 3) + 85, (Config.height / 2) + 14), Vector2.Zero);
            AddBall(new Vector2(Config.width - (Config.width / 3) + 85, (Config.height / 2) + 41), Vector2.Zero);

            AddBall(new Vector2(Config.width - (Config.width / 3) + 113, (Config.height / 2)), Vector2.Zero);
            AddBall(new Vector2(Config.width - (Config.width / 3) + 113, (Config.height / 2) - 27), Vector2.Zero);
            AddBall(new Vector2(Config.width - (Config.width / 3) + 113, (Config.height / 2) - 55), Vector2.Zero);
            AddBall(new Vector2(Config.width - (Config.width / 3) + 113, (Config.height / 2) + 27), Vector2.Zero);
            AddBall(new Vector2(Config.width - (Config.width / 3) + 113, (Config.height / 2) + 55), Vector2.Zero);

            cue = new Cue
            {
                position = new Vector2(cueBall.position.X, cueBall.position.Y),
                mass = 0,
                drawCue = true,
            };
        }

        private void AddRandomBalls()
        {
            for (int i = 0; i < 10; i++)
            {
                float x = (float)rng.Next(0, 1200);
                float y = (float)rng.Next(0, 720);
                float a = (float)rng.Next(minV, maxV);
                float b = (float)rng.Next(minV, maxV);

                if (i % 2 == 0)
                {
                    AddBall(new Vector2(x, y), new Vector2(a, b));
                }
                else
                {
                    AddBall(new Vector2(x, y), new Vector2(a, b));
                }
            }
        }

        private Entity AddBall(Vector2 position, Vector2 speed)
        {
            var ball = new Entity
            {
                position = position,
                speed = speed,
                radius = 12.5f,
                mass = 100
            };
            entities.Add(ball);

            return ball;
        }

        public void ClearBalls()
        {
            entities.Clear();
            cue.drawCue = false;
        }

        public void Update(float dt)
        {
            int width = Raylib.GetScreenWidth();
            int height = Raylib.GetScreenHeight();

            var top = new Rectangle(0, 0, width, 5);
            var bottom = new Rectangle(0, height - 5, width, 5);
            var left = new Rectangle(0, 0, 5, height);
            var right = new Rectangle(width - 5, 0, 5, height);

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].position += entities[i].speed * dt;

                if (Raylib.CheckCollisionCircleRec(entities[i].position, entities[i].radius, top))
                {
                    entities[i].speed.Y *= -1;
                }
                else if (Raylib.CheckCollisionCircleRec(entities[i].position, entities[i].radius, bottom))
                {
                    entities[i].speed.Y *= -1;
                }
                else if (Raylib.CheckCollisionCircleRec(entities[i].position, entities[i].radius, left))
                {
                    entities[i].speed.X *= -1;
                }
                else if (Raylib.CheckCollisionCircleRec(entities[i].position, entities[i].radius, right))
                {
                    entities[i].speed.X *= -1;
                }

                entities[i].Update(entities, i, dt);

                for (int j = 0; j < entities.Count; j++)
                {
                    // Makes sure ball isn't checked against itself
                    if (entities[i] != entities[j])
                    {
                        if (entities[j].speed.X > Config.maxVel) { entities[j].speed.X = Config.maxVel; }
                        if (entities[j].speed.Y > Config.maxVel) { entities[j].speed.Y = Config.maxVel; }

                        if (entities[i].Colliding(entities[j]) & Config.shouldCollide)
                        {
                            entities[i].ResolveCollision(entities[j]);
                        }
                    }
                }
            }

            if (entities.Count > 0)
            {
                cue.Update(dt, entities);
            }
        }
    }
}
