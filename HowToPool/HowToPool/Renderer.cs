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
        public void run(List<Entity> Entities, GameTime gameTime,SpriteBatch spriteBatch) 
        {
            //Draws all entities
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].draw(spriteBatch);
            
            }
        }


        public void ContentLoad(List<Entity> Entities,ContentManager content) 
        {
            //Loads all entities textures so they can be drawn
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].texture = content.Load<Texture2D>(Entities[i].fileName);
            }
            
        
        }


    }
}
