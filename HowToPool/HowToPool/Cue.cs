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

    //Class inherits Entity
    class Cue : Entity
    {
        //Prevents power being from being too high
        public Vector2 maxDistance;

        public Vector2 defaultPos;

        public Vector2 power;

        public bool released = true;

        public bool selected = false;

        public int maxPower;

        public float rotAmount = 0.02f;

        //Bounding box for Cue
        public BoundingBox box;

        //Cue constructor
        public Cue(Texture2D texture, Vector2 _vel, Vector2 _pos,float angle,float mass = 200) : base(texture, _vel, _pos,angle,mass) 
        {
            maxDistance = new Vector2(80);

            maxPower = 10;

            this.angle = 0;

            defaultPos = _pos;

            //Sets bounding box of cue relative to where it is drawn
            box = new BoundingBox(new Vector3(_pos.X-50,_pos.Y-50,0),new Vector3(_pos.X + texture.Width + 50,_pos.Y + texture.Height + 50,0));

        }

        //Default constructor for cue
        public Cue() {}


        public void allignCue(List<Ball> balls) 
        {

            if (balls[0].vel.X == 0 && balls[0].vel.Y == 0)
            {
                defaultPos.X = balls[0].pos.X - texture.Width - 50;
                defaultPos.Y = balls[0].pos.Y + texture.Height;

            }

            //If cue not selected
            if (this.selected == false)
            {
                //Set new default position
                pos = defaultPos;
            }

        }


        public void update(GameTime gameTime, MouseCursor MouseObj, List<Ball> balls)
        {
            //this.pos = this.pos + this.vel;

            allignCue(balls);
            
           
            //If player releases cue with power
            if (released == true && power.X != 0)
            {
                //this.pos += this.vel;
                balls[0].vel.X += power.X;
                power.X  = 0;


            }
            

            //Update bounding box to current pos of cue
            this.box = new BoundingBox(new Vector3(this.pos.X - 50, this.pos.Y - 50, 0), new Vector3(this.pos.X + this.texture.Width + 50, this.pos.Y + this.texture.Height + 50, 0));

            resistance();

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


            BoundingSphere sphere = new BoundingSphere(new Vector3(this.pos.X + this.texture.Width,this.pos.Y + this.texture.Height / 2,0),10);

            /*for(int i = 0; i < balls.Count;i++)
            {
                //If end of cue intersects ball
                if (balls[i].sphere.Intersects(sphere)) 
                {
                    if(this.vel.X != 0 || this.vel.Y != 0)
                    {
                        balls[i].vel.X += 5;
                    }
                }
            }*/

            if (Keyboard.GetState().IsKeyDown(Keys.Q)) 
            {
                this.angle -= rotAmount;
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                this.angle += rotAmount;

               
            }


            //If mouse cursor is over cue bounding box
            if (MouseObj.MouseOver(this.box)) 
            {
               
                //If user clicks on cue
                 if (MouseObj.mouseState.LeftButton == ButtonState.Pressed)
                 {
                     //Cue is now selected
                     selected = true;

                     //Cue isn't released
                     released = false;
                     
                     //Limits cues distamce from default
                     if ((defaultPos.X - this.pos.X) < maxDistance.X)
                     {
                         this.pos.X -= 5;

                         if (this.power.X < maxPower) 
                         {
                             this.power.X += 1;
                         }

                     }

                 }
     
            }

            //If cue released after cue has been selected
            if (MouseObj.mouseState.LeftButton == ButtonState.Released && selected == true)
            {
                released = true;

                balls[0].vel.X += power.X;

                this.pos = defaultPos;
            }
           
        }
        


    }
}
