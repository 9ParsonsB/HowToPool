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


    class DrawString
    {
        public SpriteFont Font;
        public string Text;
        public Vector2 Position;
        public Color TextColor;
        public string id;

        public DrawString(string _id,SpriteFont _Font, string _Text, Vector2 _Position)
        {
            id = _id;
            Font = _Font;
            Text = _Text;
            Position = _Position;
            TextColor = Color.Black;
        }
    }
    
    class Entity
    {

        public Vector2 vel;

        public Vector2 pos;

        public Vector2 origin;

        public float angle;

        public String fileName;

        public Texture2D texture;

        public Entity(Texture2D _texture,Vector2 _vel,Vector2 _pos,float _angle = 0) 
        {

            texture = _texture;

            vel.X = _vel.X;
            vel.Y = _vel.Y;

            pos = _pos;
            
        
        }

        public Entity() { }

        public virtual void update(GameTime gameTime) 
        {
            //Console.WriteLine(string.Format("INDEX:"+index+". POS: X:"+this.pos.X+" Y:"+this.pos.Y+". VEL: X:"+this.vel.X+" Y:"+ this.vel.Y ));
            this.pos = this.pos + this.vel;
            //Console.WriteLine(string.Format("INDEX:" + index + ". POS: X:" + this.pos.X + " Y:" + this.pos.Y + ". VEL: X:" + this.vel.X + " Y:" + this.vel.Y));

        }

        public void draw(SpriteBatch spriteBatch)
        {
          
            spriteBatch.Draw(this.texture, this.pos, null, Color.White, this.angle, this.origin, 1f, SpriteEffects.None, 0);

        }


      


       



    }
}
