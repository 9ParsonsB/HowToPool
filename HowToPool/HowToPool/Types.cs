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

        public Vector2 vel;

        public Vector2 pos;

        public Vector2 origin;

        public float angle;

        public String fileName;

        public Texture2D texture;

        public Entity(string path,Vector2 _vel,Vector2 _pos) 
        {

            fileName = path;

            vel.X = _vel.X;
            vel.Y = _vel.Y;

            pos = _pos;
        
        }

        public void update(List<Entity> Entities,int index, GameTime gameTime) 
        {
            Console.WriteLine(string.Format("INDEX:"+index+". POS: X:"+this.pos.X+" Y:"+this.pos.Y+". VEL: X:"+this.vel.X+" Y:"+ this.vel.Y ));
            this.pos = this.pos + this.vel;
            //Console.WriteLine(string.Format("INDEX:" + index + ". POS: X:" + this.pos.X + " Y:" + this.pos.Y + ". VEL: X:" + this.vel.X + " Y:" + this.vel.Y));

        }

        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, pos, null, Color.White, angle, origin, 1f, SpriteEffects.None, 0);

        }

        public class Ball : Entity
        {
            public float mass;

            public BoundingSphere sphere;

            public Ball(string path,Vector3 center,float radius,float _mass,Vector2 _vel,Vector2 _pos) : base(path,_vel,_pos) 
            {
                sphere = new BoundingSphere(center,radius);

                mass = _mass;

            }


            public void ballUpdate(List<Entity.Ball> balls,int i,GameTime gameTime)
            {

                float delta = (float)gameTime.ElapsedGameTime.TotalSeconds; 
                

                for (int j = 0; j < balls.Count; j++)
                {
                    if(colliding(balls[j]))
                    {
                        resolveCollision(balls[j]);
                    }


                }
            }

            public bool colliding(Entity.Ball ball)
            {
                float xd = this.pos.X - ball.pos.X;
                float yd = this.pos.Y - ball.pos.Y;

                float sumRadius = this.sphere.Radius + ball.sphere.Radius;
                float sqrRadius = sumRadius * sumRadius;

                float distSqr = (xd * xd) + (yd * yd);

                if (distSqr <= sqrRadius)
                {
                    return true;
                }

                return false;
            }


            public Vector2 normalize(Vector2 v) 
            {
                Vector2 u;

                u.X = v.X / v.Length();
                u.Y = v.X / v.Length();

                return u;
            }

            public float dot(Vector2 v) 
            {
                float ab = v.X * v.Y * (float)Math.Cos(0);

                return ab;
            }





            public void resolveCollision(Ball ball)
            {
                // get the mtd
                Vector2 delta = (pos - ball.pos);
                float d = delta.Length();
                //minimum translation distance to push balls apart after intersecting
                Vector2 mtd = delta * (((this.sphere.Radius + ball.sphere.Radius) - d) / d);

                // resolve intersection --
                // inverse mass quantities
                float im1 = 1 / this.mass;
                float im2 = 1 / ball.mass;

                // push-pull them apart based off their mass
                this.pos = pos + (mtd * (im1 / (im1 + im2)));
                ball.pos = ball.pos - (mtd * (im2 / (im1 + im2)));

                // impact speed
                Vector2 v = (this.vel - (ball.vel));
                float vn = dot(normalize(v)); //Normalizes vector then converts to a single value.

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
