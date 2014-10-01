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

        public Entity(string path,Vector2 _vel) 
        {

            fileName = path;

            vel = _vel;
        
        }

        public void update(List<Entity> Entities,int index) 
        {
            
            


        }

        class Ball : Entity
        {
            BoundingSphere sphere;

            int playerType;


            public Ball(String path,Vector2 _vel,int _playerType) : base(path,_vel)
            {
                playerType = _playerType;
                
            }
          
        }


        public void draw(SpriteBatch spriteBatch) 
        {
           

            spriteBatch.Draw(texture, pos, null, Color.White, angle, origin, 1f, SpriteEffects.None, 0);
        



        }



    }
}
