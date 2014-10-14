using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HowToPool
{
    class BallEvent
    {

        public delegate void CollisionEvent(Ball ball1);

        public event CollisionEvent BallCollision;

        protected virtual void OnCollision(Ball ball)
        {
            CollisionEvent handler = BallCollision;

            if (handler != null)
            {
                handler(ball);
            }
        }

        void TestEvent()
        {
             for (int j = 0; j < balls.Count; j++)
            {

                //Makes sure ball isn't checked against itself
                if (balls[i] != balls[j])
                {

                    if (balls[j].vel.X > Config.maxVel) { balls[j].vel.X = Config.maxVel; }
                    if (balls[j].vel.Y > Config.maxVel) { balls[j].vel.Y = Config.maxVel; }



                    if (balls[j].pos.X < 0 || balls[j].pos.X > 1200 - balls[j].texture.Width)
                    {

                        balls[j].vel.X *= -1;

                        if (balls[j].pos.X < 0)
                        {
                            balls[j].pos.X = 1;
                        }
                        else
                        {
                            balls[j].pos.X = 1199 - balls[j].texture.Width;
                        }

                    }

                    if (balls[j].pos.Y < 0 || balls[j].pos.Y > 700 - balls[j].texture.Height)
                    {
                        balls[j].vel.Y *= -1;

                        if (balls[j].pos.Y < 0)
                        {
                            balls[j].pos.Y = 1;
                        }
                        else
                        {
                            balls[j].pos.Y = 699 - balls[j].texture.Height;
                        }

                    }



                }


            if (colliding(balls[j]))
            {
                OnCollision(balls[j]);
            }
        }

    }
}
