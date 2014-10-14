using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HowToPool
{


    class Subscribe
    {
        BallEvent b;

        public BallSub()
        {
            b = new BallEvent();
            b.Changed += BallEvent_Chnaged;
        }
    

        void BallEvent_Changed(Ball ball1, Ball ball2)
        {
        
            // get the mtd
            Vector2 delta = (pos - ball.pos);
            float d = delta.Length();
            //minimum translation distance to push balls apart after intersecting
            Vector2 mtd = delta * (((ball2.sphere.Radius + ball.sphere.Radius) - d) / d);

            // resolve intersection --
            // inverse mass quantities
            float im1 = 1 / ball2.mass;
            float im2 = 1 / ball.mass;

            // push-pull them apart based off their mass
            ball2.vel = ball2.vel + (mtd * (im1 / (im1 + im2)));
            ball.vel = ball.vel - (mtd * (im2 / (im1 + im2)));

            ball2.vel.X = (float)Math.Round(ball2.vel.X, 1);
            ball2.vel.Y = (float)Math.Round(ball2.vel.Y, 1);

            ball.vel.X = (float)Math.Round(ball.vel.X, 1);
            ball.vel.Y = (float)Math.Round(ball.vel.Y, 1);

            // impact speed
            Vector2 v = (ball2.vel - (ball.vel));
            v.Normalize();//Normalizes vector then converts to a single value.

            float vn = Vector2.Dot(v, v);

            //Sphere intersecting but moving away from each other already
            if (vn > 0.0f) return;

            //Collision impulse
            float i = (-(1.0f + Config.resistnace) * vn) / (im1 + im2);
            Vector2 impulse = mtd * i;

            // change in momentum
            ball2.vel = ball2.vel + (impulse * im1);
            ball.vel = ball.vel - (impulse * im2);

        
        }
    }
}
