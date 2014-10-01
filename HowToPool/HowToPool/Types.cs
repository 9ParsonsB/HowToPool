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

        public Entity(float x,float y,string path) 
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



    }
}
