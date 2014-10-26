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

        //Bounding box for Cue
        public BoundingBox box;

        //Cue constructor
        public Cue(Texture2D texture, Vector2 _vel, Vector2 _pos,float angle) : base(texture, _vel, _pos,angle) 
        {
            maxDistance = new Vector2(50);

            defaultPos = _pos;

            //Sets bounding box of cue relative to where it is drawn
            box = new BoundingBox(new Vector3(_pos.X-50,_pos.Y-50,0),new Vector3(_pos.X + texture.Width + 50,_pos.Y + texture.Height + 50,0));

        }

        public Cue() {}



        public void update(GameTime gameTime, MouseCursor MouseObj, List<Ball> balls)
        {
            //this.pos = this.pos + this.vel;

            //Update bounding box to current pos of cue
            this.box = new BoundingBox(new Vector3(this.pos.X - 50, this.pos.Y - 50, 0), new Vector3(this.pos.X + this.texture.Width + 50, this.pos.Y + this.texture.Height + 50, 0));

            //Resistance code(Note:Will add into entity class soon)
            float tempX = this.vel.X;
            float tempY = this.vel.Y;

            if (Config.shouldResist)
            {
                if (this.vel.X != 0)
                {
                    if (this.vel.X < 0) { this.vel.X += Config.resistance; }


                    if (this.vel.X > 0 && tempX == this.vel.X) { this.vel.X -= Config.resistance; }
                }

                if (this.vel.Y != 0)
                {
                    if (this.vel.Y < 0) { this.vel.Y += Config.resistance; }
                    if (this.vel.Y > 0 && tempY == this.vel.Y) { this.vel.Y -= Config.resistance; }
                }
            }


            if (tempX > 0 && this.vel.X < 0 || tempX < 0 && this.vel.X > 0)
            {
                this.vel.X = 0;
            }

            if (tempY > 0 && this.vel.Y < 0 || tempY < 0 && this.vel.Y > 0)
            {
                this.vel.Y = 0;
            }

            this.vel.X = (float)Math.Round(this.vel.X, 4);
            this.vel.Y = (float)Math.Round(this.vel.Y, 4);

            this.pos.X = (float)Math.Round(this.pos.X, 4);
            this.pos.Y = (float)Math.Round(this.pos.Y, 4);



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


            this.pos += this.vel;

            BoundingSphere sphere = new BoundingSphere(new Vector3(this.pos.X + this.texture.Width,this.pos.Y + this.texture.Height,0),5);

            for(int i = 0; i < balls.Count;i++)
            {
                //If end of cue intersects ball
                if (balls[i].sphere.Intersects(sphere)) 
                {
                    balls[i].vel += this.vel;
                }
            }


            //If mouse cursor is over cue bounding box
            if (MouseObj.MouseOver(this.box)) 
            {
                //Console.WriteLine("Over cue");

                //If user clicks on cue
                if (MouseObj.mouseState.LeftButton == ButtonState.Pressed)
                {
                    //If mouse position has changed
                    // if (MouseObj.oldMouseState.X != MouseObj.mouseState.X || MouseObj.oldMouseState.Y != MouseObj.mouseState.Y) 
                    //{
                    Console.WriteLine("Mouse pressed");

                    if ((defaultPos.X - this.pos.X) < maxDistance.X)
                    {
                        this.pos.X -= 5;
                        this.vel.X += 3;
                    }

                    

                    //If mouse moved to left
                    //if (MouseObj.oldMouseState.X < MouseObj.mouseState.X) 
                    // {
                    //Move cue to left by mouse difference
                    //this.pos.X -= (MouseObj.mouseState.X - MouseObj.oldMouseState.X);    
                    //}

                    //}
                }
               

            }
           
        }
        


    }
}
