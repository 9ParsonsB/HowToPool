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

        public Vector2 cuePullSpeed = new Vector2(5,0);

        public Vector2 defaultPos;

        public Vector2 power;

        public bool released = true;

        public bool selected = false;

        public int maxPower;

        public float rotAmount = -0.02f;

        public bool drawCue = false;



        //Bounding box for Cue
        public BoundingBox box;

        //Cue constructor
        public Cue(Texture2D texture, Vector2 _vel, Vector2 _pos,float angle,float mass = 200) : base(texture, _vel, _pos,angle,mass) 
        {
            maxDistance = new Vector2(100);

            maxPower = 20;

            this.angle = 0;

            defaultPos = _pos;

            

            //Sets bounding box of cue relative to where it is drawn
            box = new BoundingBox(new Vector3(_pos.X-50,_pos.Y-50,0),new Vector3(_pos.X + texture.Width + 50,_pos.Y + texture.Height + 50,0));

        }

        //Default constructor for cue
        public Cue() {}


        public void allignCue(List<Ball> balls) 
        {

            //If the white ball is not moving
            if (balls[0].vel.X == 0 && balls[0].vel.Y == 0)
            {
                //Get top right position
                //Vector2 temp = new Vector2(this.box.Min.X + (this.box.Max.X - this.box.Min.X), this.box.Min.Y + (this.box.Max.Y - this.box.Min.Y));

            }

            

        }

        public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
        } 


        public void update(GameTime gameTime, MouseCursor MouseObj, List<Ball> balls)
        {

            allignCue(balls);

            //Output angle
            Console.WriteLine(this.angle);

            double test = (Math.Pow(texture.Width, 2) + Math.Pow(texture.Width, 2)) - (2 * (texture.Width * texture.Width) * Math.Cos(this.angle));

            test = Math.Sqrt(test);

            float t = (float)test;

            

            //Update bounding box to current pos of cue
            //this.box = new BoundingBox(new Vector3(this.pos.X - 50, this.pos.Y - 50, 0), new Vector3(this.pos.X + this.texture.Width + 50, this.pos.Y + t + 50, 0));



            this.box = new BoundingBox(new Vector3(this.pos.X, this.pos.Y, 0), new Vector3(this.pos.X + this.texture.Width, this.pos.Y + this.texture.Height, 0));
            //Console.WriteLine(this.box.Min + " " + this.box.Max);
   
            //resistance();

  
           

          

           /*if (Keyboard.GetState().IsKeyDown(Keys.Q)) 
            {
                this.angle -= rotAmount;

                this.pos = RotateAboutOrigin(this.pos, new Vector2(balls[0].sphere.Center.X,balls[0].sphere.Center.Y), this.angle);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                this.angle += rotAmount;

                this.pos = RotateAboutOrigin(this.pos, new Vector2(balls[0].sphere.Center.X,balls[0].sphere.Center.Y), this.angle);
            }//*/

            


            //If mouse cursor is over cue bounding box
            if (MouseObj.MouseOver(this.box)) 
            {

                Console.WriteLine("Over cue.");

                //If user clicks on cue
                if (MouseObj.mouseState.LeftButton == ButtonState.Pressed)
                {
                    
                     
                     //Cue is now selected
                     selected = true;
                     
                     //Cue isn't released
                     released = false;

                     if (balls[0].vel.X == 0 && balls[0].vel.Y == 0)
                     {

                         //Limits cues distamce from default
                         if ((defaultPos.X - this.pos.X) < maxDistance.X)
                         {
                             this.pos -= cuePullSpeed;

                             if (this.power.X < maxPower)
                             {
                                 this.power.X += 1;
                                 
                             }

                         }
                     }

                     

                 }
     
            }

            //If cue released after cue has been selected
            if (MouseObj.mouseState.LeftButton == ButtonState.Released && selected == true && balls[0].vel.X == 0 && balls[0].vel.Y == 0)
            {
                released = true;
                selected = false;

                balls[0].vel.X += power.X;
                balls[0].vel.Y += power.Y;

               
            }
           
        }
        


    }
}
