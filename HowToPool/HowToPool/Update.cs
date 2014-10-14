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
    class mainUpdate
    {
        Config Config = new Config();
        

        public void run(List<Entity> Entities, List<Ball> balls, GameTime gameTime) 
        {

            //Console.WriteLine(Entities.ToArray().Length);

            for (int i = 0; i < Entities.ToArray().Length; i++)
            {
                Entities[i].update(gameTime,i);
            }

            Console.WriteLine(balls.ToArray().Length);

            for (int i = 0; i < balls.ToArray().Length; i++) 
            {
                balls[i].ballUpdate(balls,i,gameTime);

            }


        }

    }
}
