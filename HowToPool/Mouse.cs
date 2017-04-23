using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HowToPool
{
    class MouseCursor : Entity
    {
        //Bounding sphere for box
        public BoundingSphere sphere;

        public MouseState mouseState;

        public MouseState oldMouseState;

  

        public MouseCursor() : base()
        {

        }

        public bool MouseOver(BoundingBox box) 
        {
            //If mouse intersects bounding box
            if (this.sphere.Intersects(box))
            {
                return true;
            }

            return false;
        }

        //Overloaded so you can pass the points of the rectangle manually
        public bool MouseOver(Vector2 v,Vector2 v2) 
        {
            //Tempary box of points supplied so intersects method can be used
            BoundingBox temp = new BoundingBox(new Vector3(v.X,v.Y,0),new Vector3(v2.X,v2.Y,0));

            //If intersects
            if (this.sphere.Intersects(temp))
            {
                return true;
            }


            return false;
        }



        public override void update(GameTime gameTime)
        {
            //Mouse updating goes here
            mouseState = Mouse.GetState();
            oldMouseState = mouseState;
  

            //Updates mouse bounding spheres position
            this.sphere.Center = new Vector3(mouseState.X,mouseState.Y,0);
            //Console.WriteLine(oldPos);
            //Console.WriteLine("Mouse Co-ords " + this.sphere.Center);
          


        }


    }
}
