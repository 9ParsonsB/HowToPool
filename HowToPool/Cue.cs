using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HowToPool
{

    //Class inherits Entity
    class Cue : Entity
    {
        //Prevents power being from being too high
        public Vector2 maxDistance;

        public Vector2 cuePullSpeed = new Vector2(100);

        public Vector2 defaultPos;

        public Vector2 power;
        public int maxPower;

        public bool released = true;
        public bool selected = false;

        public float rotAmount = -0.02f;

        public bool drawCue = false;

        //Wether the cue ball has just stopped moving
        public bool hasStopped = false;

        //Bounding box for Cue
        public BoundingBox box;

        public int degrees = 0;

        //Cue constructor
        public Cue(Texture2D texture, Vector2 _vel, Vector2 _pos,Ball ball,float angle,float mass = 200) : base(texture, _vel, _pos,angle,mass) 
        {
            maxDistance = new Vector2(500);
            maxPower = 20;
            angle = 0;
            defaultPos = _pos;
            pos = _pos;

            //Sets bounding box of cue relative to where it is drawn
            box = new BoundingBox(new Vector3(_pos.X-50,_pos.Y-50,0),new Vector3(_pos.X + texture.Width + 50,_pos.Y + texture.Height + 50,0));   
            origin = new Vector2(texture.Width + 20,texture.Height / 2);
        }

        public Cue() {}

        public void rotateCue(List<Ball> balls) 
        {
            //If the white ball is not moving
            if (balls[0].vel.X == 0 && balls[0].vel.Y == 0)
            {
                //Get top right position
                //Vector2 temp = new Vector2(this.box.Min.X + (this.box.Max.X - this.box.Min.X), this.box.Min.Y + (this.box.Max.Y - this.box.Min.Y));

                var wBall = balls[0];
                Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y); 

                Vector2 dPos = wBall.pos - mousePosition;

                this.angle = (float)Math.Atan2(-dPos.Y, -dPos.X);

                Vector2 ratio = wBall.pos / mousePosition;

                //this.pos = wBall.pos - new Vector2(this.texture.Width + 5, 0);

                double degereese = angle * (180.0 / Math.PI);

                double cueAngle = 180.0 - (degereese + 90.0);

                cueAngle = Math.PI * cueAngle / 180.0;

            }
         
        }

        public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
        } 


        public void update(GameTime gameTime, MouseCursor MouseObj, List<Ball> balls)
        {

            if (angle < 0)
            {
                degrees = (int)((6.28318531 - (angle * -1)) * 57.2957795);
            }
            else 
            {
                degrees = (int)(angle * 57.2957795);
            }

            //Console.WriteLine(degrees);

            if (balls[0].vel.X == 0 && balls[0].vel.Y == 0)
            {
                //Rotates cue based on mouse position whenever cue ball not moving
                rotateCue(balls);

                //If cue ball just stopped moving
                if (!hasStopped)
                {                   
                    //Set pos to current pos of cue ball
                    pos = new Vector2(balls[0].pos.X + balls[0].texture.Width / 2, balls[0].pos.Y + balls[0].texture.Height / 2);
                    defaultPos = pos;

                    hasStopped = true;
                }
            }
            else
            {
                hasStopped = false;       
            }
            

            //If user clicks on cue
            if (MouseObj.mouseState.LeftButton == ButtonState.Pressed && !selected)
            {                                      
                //Cue is now selected
                selected = true;
                     
                //Cue isn't released
                released = false;            
            }

            if (selected) 
            {
                if (balls[0].vel.X == 0 && balls[0].vel.Y == 0)
                {
                    //If cue on left side of ball
                    if (degrees < 90 || degrees < 360 && degrees > 270)
                    {
                        this.pos.X -= cuePullSpeed.X * Config.delta;

                        if (this.power.X < maxPower)
                        {
                            this.power.X += 10 * Config.delta;
                        }

                        //Increase velocity of cue
                        vel.X += power.X;
                    }
                    else                   
                    {
                        //If cue on right side of ball
                        if (degrees > 90 && degrees < 270)
                        {
                            this.pos.X += cuePullSpeed.X * Config.delta;

                            if (this.power.X < maxPower)
                            {
                                this.power.X -= 10 * Config.delta;
                            }

                            //Increase velocity of cue
                            vel.X -= power.X;
                        }
                    }


                    //If cue on top side of ball
                    if (degrees > 0 && degrees < 180)
                    {
                        this.pos.Y -= cuePullSpeed.Y * Config.delta;

                        if (this.power.Y < maxPower)
                        {
                            this.power.Y += 10 * Config.delta;
                        }

                        //Increase velocity of cue
                        vel.Y += power.Y;
                    }
                    else
                    {
                        //If cue on bottom side of ball
                        if (degrees > 180)
                        {
                            this.pos.Y += cuePullSpeed.Y * Config.delta;

                            if (this.power.Y < maxPower)
                            {
                                this.power.Y -= 10 * Config.delta;
                            }

                            //Increase velocity of cue
                            vel.Y -= power.Y;
                        }
                    }

         
                }         

            }

            //If cue released after cue has been selected
            if (MouseObj.mouseState.LeftButton == ButtonState.Released && selected == true && balls[0].vel.X == 0 && balls[0].vel.Y == 0)
            {             
                released = true;
                selected = false;                      
            }
           
            if(released)
            {
               
                //If cue has not returned to ball
                if (pos.X < defaultPos.X || pos.Y < defaultPos.Y)
                {                 
                    //Move cue back towards ball
                    pos += vel * Config.delta;
                }
                else 
                {
                    Console.WriteLine(power);

                    //Apply power to ball
                    balls[0].vel.X += power.X;
                    balls[0].vel.Y += power.Y;

                    //Reset released
                    released = false;

                    vel = new Vector2(0,0);
                    power = new Vector2(0,0);
                }
            }

        }     

    }
}
