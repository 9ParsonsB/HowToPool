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
    class Update
    {
        Config Config = new Config();

        public void run(List<Entity> Entities,GameTime gameTime) 
        {
            for (int i = 0; i < Entities.Count; i++) 
            {
                if (Entities[i].GetType() == balls[0].GetType())
                {
                    Entities[i].update(balls, i);
                }
                else
                {
                    Entities[i].update(Entities,i);
                }
            }
        }
    }
}
