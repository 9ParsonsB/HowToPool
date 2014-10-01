using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HowToPool
{
    class Player
    {
        public string name; //TODO: give use chance to input name
        //public Entity Entity;
        public int BallsRemaining;
        public string colour;
        public bool isTurn;
    }

    class State 
    {
        public string name;
    }

    
    class Entity
    {
     
        Vector2 vel;

        Vector2 pos;

        Vector2 origin;

        float angle;

        public String fileName;

        public Texture2D texture;

        public Entity(string path,Vector2 _vel) 
        {

            fileName = path;

            vel.X = x;
            vel.Y = y;
        
        }

        public void update(List<Entity> Entities,int index) 
        {
        
            /*if(Entities[])
            {
                
            }*/

        }

        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, pos, null, Color.White, angle, origin, 1f, SpriteEffects.None, 0);

        }

        class Ball : Entity
        {
            float mass;

            BoundingSphere sphere;

            public Ball(string path, Vector2 _vel) : base(path,_vel) 
            {
            

            }

            public bool colliding(Ball ball)
            {
                float xd = this.pos.X - ball.pos.X;
                float yd = this.pos.Y - ball.pos.Y;

                float sumRadius = this.sphere + ball.sphere;
                float sqrRadius = this.sumRadius * this.sumRadius;

                float distSqr = (xd * xd) + (yd * yd);

                if (distSqr <= sqrRadius)
                {
                    return true;
                }

                return false;
            }

            public void resolveCollision(Ball ball)
            {
                // get the mtd
                Vector2 delta = (pos - ball.pos);
                float d = delta.getLength();
                // minimum translation distance to push balls apart after intersecting
                Vector2 mtd = delta * (((this.sphere + ball.sphere) - d) / d);


                // resolve intersection --
                // inverse mass quantities
                float im1 = 1 / this.mass;
                float im2 = 1 / ball.mass;

                // push-pull them apart based off their mass
                this.pos = pos + (mtd * (im1 / (im1 + im2)));
                ball.pos = ball.pos - (mtd * (im2 / (im1 + im2)));

                // impact speed
                Vector2 v = (this.vel - (ball.vel));
                float vn = v.dot(mtd.normalize()); // WTF?

                // sphere intersecting but moving away from each other already
                if (vn > 0.0f) return;

                // collision impulse
                float i = (-(1.0f + Config.resistnace) * vn) / (im1 + im2);
                Vector2 impulse = mtd * i;

                // change in momentum
                this.vel = this.vel + (impulse* im1);
                ball.vel = ball.vel - (impulse * im2);

            }

        }


       



    }
}
