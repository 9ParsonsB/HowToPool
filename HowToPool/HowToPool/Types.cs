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
        public Entity Entity;
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


        public void update(List<Entity> Entities,int index) 
        {
            
            /*if(Entities[])
            {
                
            }*/


        }

        class Ball
        {
            public Texture2D texture;
            public Vector3 pos;
            public Vector2 vel;
            public 
        }


    }
}
