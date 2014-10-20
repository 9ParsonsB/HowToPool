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
        public Vector2 maxVel;

        //Bounding box for Cue
        public BoundingBox box;

        //Cue constructor
        public Cue(Texture2D texture, Vector2 _vel, Vector2 _pos,float angle) : base(texture, _vel, _pos,angle) 
        {
            

        }

     



        public void update(GameTime gameTime,MouseCursor MouseObj)
        {
            //this.pos = this.pos + this.vel;

            if (MouseObj.MouseOver(this.box)) 
            {
                Console.WriteLine("Over cue");
            }
           
        }
        


    }
}
