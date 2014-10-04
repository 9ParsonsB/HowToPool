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

        public void update(GameTime gameTime, int index) 
        {
            //Console.WriteLine(string.Format("INDEX:"+index+". POS: X:"+this.pos.X+" Y:"+this.pos.Y+". VEL: X:"+this.vel.X+" Y:"+ this.vel.Y ));
            this.pos = this.pos + this.vel;
            //Console.WriteLine(string.Format("INDEX:" + index + ". POS: X:" + this.pos.X + " Y:" + this.pos.Y + ". VEL: X:" + this.vel.X + " Y:" + this.vel.Y));

        }

        public void draw(SpriteBatch spriteBatch)
        {
          
            spriteBatch.Draw(this.texture, this.pos, null, Color.White, this.angle, this.origin, 1f, SpriteEffects.None, 0);

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


            public void ballUpdate(List<Entity.Ball> balls, int i, GameTime gameTime)
            {

                //Console.WriteLine(this.pos);
                //Console.WriteLine(this.vel);

                this.pos = this.pos + this.vel;

                if (this.pos.X < 0 || this.pos.X > 1200 - this.texture.Width) 
                { 
                    this.vel.X *= -1;
                    if (this.pos.X < 0)
                    {
                        this.pos.X = 1;
                    }
                    else 
                    {
                        this.pos.X = 1199 - this.texture.Width;
                    }   

                }
               
                if (this.pos.Y < 0 || this.pos.Y > 700 - this.texture.Height) 
                {  
                    this.vel.Y *= -1;

                    if (this.pos.Y < 0)
                    {
                        this.pos.Y = 1;
                    }
                    else 
                    {
                        this.pos.Y = 699 - this.texture.Height;
                    }

                }

       

                this.sphere.Center = new Vector3(this.pos.X, this.pos.Y, 0);

                float delta = (float)gameTime.ElapsedGameTime.TotalSeconds; 
                

                for (int j = 0; j < balls.Count; j++)
                {

                    if (balls[i] != balls[j]) {

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

                        if(colliding(balls[j]))
                        {
                            resolveCollision(balls[j]);
                        }

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

                if (distSqr <= sqrRadius) // causing errors as should not return true
                {
                    return true;
                }

                return false;
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
                this.vel = this.vel + (mtd * (im1 / (im1 + im2)));
                ball.vel = ball.vel - (mtd * (im2 / (im1 + im2)));

                this.vel.X = (float)Math.Round(this.vel.X, 1);
                this.vel.Y = (float)Math.Round(this.vel.Y, 1);

                ball.vel.X = (float)Math.Round(ball.vel.X, 1);
                ball.vel.Y = (float)Math.Round(ball.vel.Y, 1);

                // impact speed
                Vector2 v = (this.vel - (ball.vel));
                v.Normalize();//Normalizes vector then converts to a single value.

                float vn = Vector2.Dot(v,v);

                //Sphere intersecting but moving away from each other already
                if (vn > 0.0f) return;

                //Collision impulse
                float i = (-(1.0f + Config.resistnace) * vn) / (im1 + im2);
                Vector2 impulse = mtd * i;

                // change in momentum
                this.vel = this.vel + (impulse * im1);
                ball.vel = ball.vel - (impulse * im2);
               

                

                
                
         


                
     

            }

        }


       



    }
}
