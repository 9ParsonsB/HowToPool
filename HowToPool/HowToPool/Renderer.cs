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
    class Renderer
    {
        public void run(List<Entity> Entities,List<Ball> balls,Cue cue,GameTime gameTime,SpriteBatch spriteBatch) 
        {
            //Draws all entities
            /*for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].draw(spriteBatch);
            
            }*/

            for (int i = 0; i < balls.Count; i++) 
            {
                balls[i].draw(spriteBatch);
            }

            cue.draw(spriteBatch);
        }


        public List<Texture2D> ContentLoad(List<Entity> Entities,List<Ball> balls,Cue cue,ContentManager content) 
        {
            //Loads all entities textures so they can be drawn
            List<Texture2D> textures = new List<Texture2D>();

            textures.Add(content.Load<Texture2D>("blueBall"));
            
            textures.Add(content.Load<Texture2D>("redBall"));

            textures.Add(content.Load<Texture2D>("whiteBall"));

            cue.texture = content.Load<Texture2D>("cue");
            

            return textures;


        
        }


    }
}

